import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import * as authApi from '@/api/auth';
import { getCookie, deleteCookie } from '@/utils/cookie';

export interface User {
  id: number;
  email: string;
  phoneNumber?: string;
}

export const useAuthStore = defineStore('auth', () => {
  // State
  const user = ref<User | null>(null);
  const accessToken = ref<string | null>(localStorage.getItem('accessToken'));
  // Initialize isAuthenticated based on accessToken only (user will be populated by verify)
  const isAuthenticated = computed(() => !!accessToken.value);
  const isLoading = ref(false);
  const error = ref<string | null>(null);

  // Actions
  async function loginStep1(email: string): Promise<{ success: boolean; message?: string }> {
    try {
      error.value = null;
      console.log('[Auth Store] Calling loginStep1 with email:', email);
      const response = await authApi.loginStep1(email);
      console.log('[Auth Store] loginStep1 response:', response);
      return { success: response.success };
    } catch (e: unknown) {
      console.error('[Auth Store] loginStep1 error:', e);
      const err = e as { response?: { data?: { message?: string } }; message?: string };
      error.value = err.response?.data?.message || err.message || 'Failed to verify email';
      console.error('[Auth Store] error.value:', error.value);
      return { success: false, message: error.value };
    }
  }

  async function login(email: string, password: string): Promise<{ success: boolean; message?: string }> {
    try {
      error.value = null;
      const response = await authApi.login({ email, password });

      if (response.success) {
        // Store access token
        accessToken.value = response.accessToken;
        localStorage.setItem('accessToken', response.accessToken);

        // Set user info
        user.value = {
          id: response.user.id,
          email: response.user.email,
        };

        // Save user to localStorage for persistence
        localStorage.setItem('user', JSON.stringify(user.value));
      }

      return { success: response.success, message: response.message };
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      error.value = err.response?.data?.message || 'Login failed';
      return { success: false, message: error.value };
    }
  }

  async function register(email: string, password: string, phoneNumber?: string): Promise<{ success: boolean; userId?: number; message?: string }> {
    try {
      error.value = null;
      const response = await authApi.register({ email, password, phoneNumber });
      return response;
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      error.value = err.response?.data?.message || 'Registration failed';
      return { success: false, message: error.value };
    }
  }

  async function logout(): Promise<void> {
    try {
      await authApi.logout();
    } catch (e) {
      console.error('Logout API call failed:', e);
    } finally {
      // Always clear local state
      accessToken.value = null;
      user.value = null;
      localStorage.removeItem('accessToken');
      deleteCookie('refreshToken', '/api/auth');
    }
  }

  async function verify(): Promise<boolean> {
    if (!accessToken.value) {
      return false;
    }

    try {
      isLoading.value = true;
      const response = await authApi.verifyToken();

      if (response.valid) {
        user.value = {
          id: response.user.id,
          email: response.user.email,
          phoneNumber: response.user.phoneNumber,
        };
        return true;
      }

      return false;
    } catch (e) {
      console.error('Token verification failed:', e);
      return false;
    } finally {
      isLoading.value = false;
    }
  }

  async function passwordRecovery(email: string, method: 'sms' | 'email'): Promise<{ success: boolean; codeId?: string; message?: string }> {
    try {
      error.value = null;
      const response = await authApi.passwordRecovery({ email, method });
      return { success: response.success, codeId: response.codeId, message: response.message };
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      error.value = err.response?.data?.message || 'Password recovery failed';
      return { success: false, message: error.value };
    }
  }

  async function verifyCode(codeId: string, code: string): Promise<{ success: boolean; resetToken?: string; message?: string }> {
    try {
      error.value = null;
      const response = await authApi.verifyCode({ codeId, code });
      return { success: response.success, resetToken: response.resetToken, message: response.message };
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      error.value = err.response?.data?.message || 'Code verification failed';
      return { success: false, message: error.value };
    }
  }

  async function resetPassword(resetToken: string, newPassword: string): Promise<{ success: boolean; message?: string }> {
    try {
      error.value = null;
      const response = await authApi.resetPassword({ resetToken, newPassword });
      return { success: response.success, message: response.message };
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      error.value = err.response?.data?.message || 'Password reset failed';
      return { success: false, message: error.value };
    }
  }

  // Initialize auth state on app load
  async function initAuth(): Promise<void> {
    if (accessToken.value) {
      // Token exists, verify it
      const valid = await verify();
      if (!valid) {
        // Token invalid, clear state
        accessToken.value = null;
        user.value = null;
        localStorage.removeItem('accessToken');
        localStorage.removeItem('user');
      }
    } else {
      // No token, try to restore user from localStorage for UI display
      const savedUser = localStorage.getItem('user');
      if (savedUser) {
        try {
          user.value = JSON.parse(savedUser);
        } catch (e) {
          localStorage.removeItem('user');
        }
      }
    }
  }

  return {
    // State
    user,
    accessToken,
    isAuthenticated,
    isLoading,
    error,
    // Actions
    loginStep1,
    login,
    register,
    logout,
    verify,
    initAuth,
    passwordRecovery,
    verifyCode,
    resetPassword,
  };
});
