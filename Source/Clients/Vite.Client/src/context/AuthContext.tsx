// src/contexts/AuthContext.tsx
import React, {
  createContext,
  useContext,
  ReactNode,
  useEffect,
  useState,
  useCallback,
} from "react"

import { useNavigate } from "react-router-dom"
import axios from "axios"
import { addAuthToken, setup401Interceptor } from "../api/axios"
import { UserType, AuthState } from "../types"
// Use your BO client
import { ParentsClient,StudentsClient, StaffClient  } from "../api/BO-api.service"
import { ParentDTO, StudentDTO, StaffDTO } from "../api/BO-api.service"

interface AuthContextType extends AuthState {
  login: (username: string, password: string, userType: UserType) => Promise<void>
  logout: () => void
  isLoading: boolean
  error: string | null
}

const initialState: AuthState = {
  isAuthenticated: false,
  userType: null,
  username: null,
  token: null,
  refreshToken: null,
}

const TOKEN_KEY     = "edulink_token"
const REFRESH_KEY   = "edulink_refresh"
const USER_KEY      = "edulink_user"
const USER_ID_KEY   = "edulink_userId"
const PARENT_ID_KEY = "edulink_parentId"
const STUDENT_ID_KEY= "edulink_studentId"
const STAFF_ID_KEY  = "edulink_staffId"

export const AuthContext = createContext<AuthContextType>({
  ...initialState,
  login: async () => {},
  logout: () => {},
  isLoading: false,
  error: null,
})
export const useAuth = () => useContext(AuthContext)

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [auth, setAuth]         = useState<AuthState>(initialState)
  const [isLoading, setLoading] = useState(true)
  const [error, setError]       = useState<string | null>(null)
  const navigate = useNavigate()
  const authAxios = axios.create({
  baseURL: import.meta.env.VITE_BACKOFFICE_API_URL,
  headers: {
    Authorization: auth.token ? `Bearer ${auth.token}` : undefined,
  },
})
const ParentClient  = new ParentsClient(undefined, authAxios)
const StudentClient = new StudentsClient(undefined, authAxios)
const StaffsClient  = new StaffClient(undefined, authAxios)


  // 1️⃣ Logout declared first, wrapped in useCallback
  const logout = useCallback(() => {
    setAuth(initialState)
    localStorage.removeItem(TOKEN_KEY)
    localStorage.removeItem(REFRESH_KEY)
    localStorage.removeItem(USER_KEY)
    localStorage.removeItem(USER_ID_KEY)
    localStorage.removeItem(PARENT_ID_KEY)
    localStorage.removeItem(STUDENT_ID_KEY)
    localStorage.removeItem(STAFF_ID_KEY)
    addAuthToken(null)
    navigate("/login")
  }, [navigate])

  // 2️⃣ Hydrate auth on mount
  useEffect(() => {
    const token        = localStorage.getItem(TOKEN_KEY)
    const refreshToken = localStorage.getItem(REFRESH_KEY)
    const storedUser   = localStorage.getItem(USER_KEY)
    const storedUserId = localStorage.getItem(USER_ID_KEY)
    if (token && refreshToken && storedUser && storedUserId) {
      const { username, userType } = JSON.parse(storedUser)
      setAuth({ isAuthenticated: true, username, userType, token, refreshToken })
      addAuthToken(token)
    }
    setLoading(false)
  }, [])

  // 3️⃣ 401 interceptor
  useEffect(() => {
    if (!auth.refreshToken) return
    const unregister = setup401Interceptor(
      auth.refreshToken,
      (newToken, newRefresh) => {
        localStorage.setItem(TOKEN_KEY, newToken)
        localStorage.setItem(REFRESH_KEY, newRefresh)
        setAuth(a => ({ ...a, token: newToken, refreshToken: newRefresh }))
        addAuthToken(newToken)
      },
      logout
    )
    return unregister
  }, [auth.refreshToken, logout])

  // 4️⃣ Helper to extract JWT sub
  function parseJwtSub(token: string): string | null {
    try {
      const payload = token.split(".")[1]
      const decoded = atob(payload.replace(/-/g, "+").replace(/_/g, "/"))
      return JSON.parse(decoded).sub
    } catch {
      return null
    }
  }

  // 5️⃣ Login
  const login = async (username: string, password: string, userType: UserType) => {
    setLoading(true)
    setError(null)
    try {
      const base = import.meta.env.VITE_IDENTITY_API_URL
      const resp = await axios.post(`${base}/identity/login`, {
        username,
        password,
        userType,
      })
      const { access_token: token, refresh_token: refreshToken } = resp.data
      if (!token || !refreshToken) throw new Error("Missing tokens")

      const userId = parseJwtSub(token)
      if (!userId) throw new Error("Invalid token")

      // Persist core
      localStorage.setItem(TOKEN_KEY, token)
      localStorage.setItem(REFRESH_KEY, refreshToken)
      localStorage.setItem(USER_KEY, JSON.stringify({ username, userType }))
      localStorage.setItem(USER_ID_KEY, userId)

      setAuth({ isAuthenticated: true, username, userType, token, refreshToken })
      addAuthToken(token)

      // Fetch BO IDs
      if (userType === "Parent") {
        const parentRec = await ParentClient.getParentByUserId(userId)
        localStorage.setItem(PARENT_ID_KEY, parentRec.id ?? "")
      } else if (userType === "Student") {
        const studentRec = await StudentClient.getByUserId(userId)
        localStorage.setItem(STUDENT_ID_KEY, studentRec.id ?? "")
      } else {
        const staffRec: StaffDTO = await StaffsClient.getByUserId(userId)
        localStorage.setItem(STAFF_ID_KEY, staffRec.id ?? "")
      }

      // Route
      switch (userType) {
        case "Student":
          navigate("/student/dashboard")
          break
        case "Parent":
          navigate("/parent/dashboard")
          break
        default:
          navigate("/backoffice/dashboard")
      }
    } catch (e) {
      const msg =
        axios.isAxiosError(e) && e.response?.data?.message
          ? e.response.data.message
          : e instanceof Error
          ? e.message
          : "Login failed"
      setError(msg)
    } finally {
      setLoading(false)
    }
  }

  // 6️⃣ Redirect unauthenticated
  useEffect(() => {
    if (!auth.isAuthenticated && !isLoading) {
      navigate("/login")
    }
  }, [auth.isAuthenticated, isLoading, navigate])

  return (
    <AuthContext.Provider value={{ ...auth, login, logout, isLoading, error }}>
      {children}
    </AuthContext.Provider>
  )
}
