/**
 * Cookie utility functions for handling HttpOnly cookies
 */

export function getCookie(name: string): string | null {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) {
    return parts.pop()?.split(';').shift() || null;
  }
  return null;
}

export function setCookie(name: string, value: string, options?: {
  expires?: Date;
  path?: string;
  domain?: string;
  secure?: boolean;
  sameSite?: 'Strict' | 'Lax' | 'None';
}): void {
  let cookieString = `${name}=${value}`;

  if (options?.expires) {
    cookieString += `; expires=${options.expires.toUTCString()}`;
  }

  if (options?.path) {
    cookieString += `; path=${options.path}`;
  }

  if (options?.domain) {
    cookieString += `; domain=${options.domain}`;
  }

  if (options?.secure) {
    cookieString += `; secure`;
  }

  if (options?.sameSite) {
    cookieString += `; SameSite=${options.sameSite}`;
  }

  document.cookie = cookieString;
}

export function deleteCookie(name: string, path?: string): void {
  let cookieString = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT`;

  if (path) {
    cookieString += `; path=${path}`;
  }

  document.cookie = cookieString;
}
