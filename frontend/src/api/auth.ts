import axios from 'axios';

const API_BASE_URL = 'http://localhost:9000/api/auth';

// Create axios instance
const authApi = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true, // Send cookies with requests
});

// Request interceptor to add auth token
authApi.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor to handle token refresh
authApi.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // If error is 401 and we haven't retried yet
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const response = await axios.post(
          `${API_BASE_URL}/refresh`,
          {},
          { withCredentials: true }
        );

        if (response.data.success) {
          const { accessToken } = response.data;
          localStorage.setItem('accessToken', accessToken);

          // Retry original request with new token
          originalRequest.headers.Authorization = `Bearer ${accessToken}`;
          return authApi(originalRequest);
        }
      } catch (refreshError) {
        // Refresh failed - clear auth and redirect to login
        localStorage.removeItem('accessToken');
        window.location.href = '/login';
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

// Types
export interface RegisterRequest {
  email: string;
  password: string;
  phoneNumber?: string;
}

export interface LoginStep1Request {
  email: string;
}

export interface LoginStep1Response {
  success: true;
  requiresPassword: true;
  message: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  user: {
    id: number;
    email: string;
  };
  accessToken: string;
  expiresIn: number;
}

export interface PasswordRecoveryRequest {
  email: string;
  method: 'sms' | 'email';
}

export interface PasswordRecoveryResponse {
  success: boolean;
  codeId: string;
  message: string;
}

export interface VerifyCodeRequest {
  codeId: string;
  code: string;
}

export interface VerifyCodeResponse {
  success: boolean;
  resetToken: string;
  message: string;
}

export interface ResetPasswordRequest {
  resetToken: string;
  newPassword: string;
}

export interface VerifyTokenResponse {
  valid: boolean;
  user: {
    id: number;
    email: string;
    phoneNumber?: string;
  };
}

// API Methods
export async function register(data: RegisterRequest): Promise<{ success: boolean; userId?: number; message?: string }> {
  const response = await authApi.post('/register', data);
  return response.data;
}

export async function loginStep1(email: string): Promise<LoginStep1Response> {
  const response = await authApi.post('/login-step1', { email });
  return response.data;
}

export async function login(data: LoginRequest): Promise<LoginResponse> {
  const response = await authApi.post('/login', data);
  return response.data;
}

export async function logout(): Promise<{ success: boolean; message: string }> {
  const response = await authApi.post('/logout');
  return response.data;
}

export async function refresh(): Promise<{ success: boolean; accessToken: string; expiresIn: number }> {
  const response = await authApi.post('/refresh');
  return response.data;
}

export async function passwordRecovery(data: PasswordRecoveryRequest): Promise<PasswordRecoveryResponse> {
  const response = await authApi.post('/password-recovery', data);
  return response.data;
}

export async function verifyCode(data: VerifyCodeRequest): Promise<VerifyCodeResponse> {
  const response = await authApi.post('/verify-code', data);
  return response.data;
}

export async function resetPassword(data: ResetPasswordRequest): Promise<{ success: boolean; message: string }> {
  const response = await authApi.post('/reset-password', data);
  return response.data;
}

export async function verifyToken(): Promise<VerifyTokenResponse> {
  const response = await authApi.get('/verify');
  return response.data;
}
