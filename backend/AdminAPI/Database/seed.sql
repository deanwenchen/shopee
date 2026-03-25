-- ============================================
-- Shoppe Admin Database Seed Data Script
-- ============================================
-- This script inserts initial data for the admin system
-- ============================================

USE shoppe_admin;

-- ============================================
-- 1. Default Admin Account
-- ============================================
-- Username: admin
-- Password: Admin@123
-- Email: admin@shoppe.com
-- Status: 1 (enabled)
-- ============================================

INSERT INTO `admin_users` (`Username`, `PasswordHash`, `Email`, `Phone`, `Status`, `CreatedAt`, `UpdatedAt`)
VALUES (
    'admin',
    '$2a$10$Rp/ZkNeArQW.uzikrDsm1.cUJwiRDEBNbNSKQ0mzYKzPz7PYfn5dW',
    'admin@shoppe.com',
    NULL,
    1,
    UTC_TIMESTAMP(),
    UTC_TIMESTAMP()
);

-- ============================================
-- 2. Base Permissions (Hierarchical Structure)
-- ============================================
-- Type: 1=Directory, 2=Menu, 3=Button, 4=API
-- ============================================

-- Level 1: Directories (type=1)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(1, 'Dashboard', 'dashboard', 1, NULL, '/dashboard', 'Dashboard', NULL, 1, UTC_TIMESTAMP()),
(2, 'System Management', 'system', 1, NULL, '/system', 'Settings', NULL, 2, UTC_TIMESTAMP()),
(3, 'User Management', 'user', 1, NULL, '/users', 'User', NULL, 3, UTC_TIMESTAMP()),
(4, 'Logs', 'logs', 1, NULL, '/logs', 'Document', NULL, 4, UTC_TIMESTAMP());

-- Level 2: Menus under Dashboard (type=2)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(5, 'Dashboard Home', 'dashboard:home', 2, 1, '/dashboard', 'Home', NULL, 1, UTC_TIMESTAMP());

-- Level 2: Menus under System Management (type=2)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(6, 'Admin Management', 'admin', 2, 2, '/system/admins', 'User', NULL, 1, UTC_TIMESTAMP()),
(7, 'Role Management', 'role', 2, 2, '/system/roles', 'Lock', NULL, 2, UTC_TIMESTAMP()),
(8, 'Permission Management', 'permission', 2, 2, '/system/permissions', 'Key', NULL, 3, UTC_TIMESTAMP());

-- Level 2: Menus under User Management (type=2)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(9, 'User List', 'user:list', 2, 3, '/users', 'Users', NULL, 1, UTC_TIMESTAMP());

-- Level 2: Menus under Logs (type=2)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(10, 'Operation Logs', 'logs:operation', 2, 4, '/logs', 'History', NULL, 1, UTC_TIMESTAMP());

-- Level 3: Buttons under Admin Management (type=3)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(11, 'Admin Create', 'admin:create', 3, 6, NULL, 'Plus', NULL, 1, UTC_TIMESTAMP()),
(12, 'Admin Edit', 'admin:update', 3, 6, NULL, 'Edit', NULL, 2, UTC_TIMESTAMP()),
(13, 'Admin Delete', 'admin:delete', 3, 6, NULL, 'Delete', NULL, 3, UTC_TIMESTAMP()),
(14, 'Admin Status', 'admin:status', 3, 6, NULL, 'Switch', NULL, 4, UTC_TIMESTAMP()),
(15, 'Admin Reset Password', 'admin:reset-pwd', 3, 6, NULL, 'Refresh', NULL, 5, UTC_TIMESTAMP()),
(16, 'Admin Detail', 'admin:detail', 3, 6, NULL, 'View', NULL, 6, UTC_TIMESTAMP());

-- Level 3: Buttons under Role Management (type=3)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(17, 'Role Create', 'role:create', 3, 7, NULL, 'Plus', NULL, 1, UTC_TIMESTAMP()),
(18, 'Role Edit', 'role:update', 3, 7, NULL, 'Edit', NULL, 2, UTC_TIMESTAMP()),
(19, 'Role Delete', 'role:delete', 3, 7, NULL, 'Delete', NULL, 3, UTC_TIMESTAMP()),
(20, 'Role Assign Permissions', 'role:assign', 3, 7, NULL, 'Lock', NULL, 4, UTC_TIMESTAMP()),
(21, 'Role Detail', 'role:detail', 3, 7, NULL, 'View', NULL, 5, UTC_TIMESTAMP());

-- Level 3: Buttons under Permission Management (type=3)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(22, 'Permission Create', 'permission:create', 3, 8, NULL, 'Plus', NULL, 1, UTC_TIMESTAMP()),
(23, 'Permission Edit', 'permission:update', 3, 8, NULL, 'Edit', NULL, 2, UTC_TIMESTAMP()),
(24, 'Permission Delete', 'permission:delete', 3, 8, NULL, 'Delete', NULL, 3, UTC_TIMESTAMP()),
(25, 'Permission Detail', 'permission:detail', 3, 8, NULL, 'View', NULL, 4, UTC_TIMESTAMP());

-- Level 3: Buttons under User List (type=3)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(26, 'User Create', 'user:create', 3, 9, NULL, 'Plus', NULL, 1, UTC_TIMESTAMP()),
(27, 'User Edit', 'user:update', 3, 9, NULL, 'Edit', NULL, 2, UTC_TIMESTAMP()),
(28, 'User Delete', 'user:delete', 3, 9, NULL, 'Delete', NULL, 3, UTC_TIMESTAMP()),
(29, 'User Detail', 'user:detail', 3, 9, NULL, 'View', NULL, 4, UTC_TIMESTAMP());

-- Level 3: Buttons under Operation Logs (type=3)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(30, 'Log Detail', 'logs:detail', 3, 10, NULL, 'View', NULL, 1, UTC_TIMESTAMP());

-- Level 4: API Permissions for Admin Management (type=4)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(31, 'API: Get Admins', 'admin:list', 4, 6, NULL, NULL, 'GET /api/admin', 1, UTC_TIMESTAMP()),
(32, 'API: Get Admin', 'admin:detail', 4, 6, NULL, NULL, 'GET /api/admin/{id}', 2, UTC_TIMESTAMP()),
(33, 'API: Create Admin', 'admin:create', 4, 6, NULL, NULL, 'POST /api/admin', 3, UTC_TIMESTAMP()),
(34, 'API: Update Admin', 'admin:update', 4, 6, NULL, NULL, 'PUT /api/admin/{id}', 4, UTC_TIMESTAMP()),
(35, 'API: Delete Admin', 'admin:delete', 4, 6, NULL, NULL, 'DELETE /api/admin/{id}', 5, UTC_TIMESTAMP()),
(36, 'API: Update Admin Status', 'admin:status', 4, 6, NULL, NULL, 'PUT /api/admin/{id}/status', 6, UTC_TIMESTAMP()),
(37, 'API: Reset Admin Password', 'admin:reset-pwd', 4, 6, NULL, NULL, 'POST /api/admin/{id}/reset-password', 7, UTC_TIMESTAMP());

-- Level 4: API Permissions for Role Management (type=4)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(38, 'API: Get Roles', 'role:list', 4, 7, NULL, NULL, 'GET /api/role', 1, UTC_TIMESTAMP()),
(39, 'API: Get Role', 'role:detail', 4, 7, NULL, NULL, 'GET /api/role/{id}', 2, UTC_TIMESTAMP()),
(40, 'API: Create Role', 'role:create', 4, 7, NULL, NULL, 'POST /api/role', 3, UTC_TIMESTAMP()),
(41, 'API: Update Role', 'role:update', 4, 7, NULL, NULL, 'PUT /api/role/{id}', 4, UTC_TIMESTAMP()),
(42, 'API: Delete Role', 'role:delete', 4, 7, NULL, NULL, 'DELETE /api/role/{id}', 5, UTC_TIMESTAMP()),
(43, 'API: Assign Permissions to Role', 'role:assign', 4, 7, NULL, NULL, 'PUT /api/role/{id}/permissions', 6, UTC_TIMESTAMP());

-- Level 4: API Permissions for Permission Management (type=4)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(44, 'API: Get Permissions', 'permission:list', 4, 8, NULL, NULL, 'GET /api/permission', 1, UTC_TIMESTAMP()),
(45, 'API: Get Permission Tree', 'permission:tree', 4, 8, NULL, NULL, 'GET /api/permission/tree', 2, UTC_TIMESTAMP()),
(46, 'API: Get Permission', 'permission:detail', 4, 8, NULL, NULL, 'GET /api/permission/{id}', 3, UTC_TIMESTAMP()),
(47, 'API: Create Permission', 'permission:create', 4, 8, NULL, NULL, 'POST /api/permission', 4, UTC_TIMESTAMP()),
(48, 'API: Update Permission', 'permission:update', 4, 8, NULL, NULL, 'PUT /api/permission/{id}', 5, UTC_TIMESTAMP()),
(49, 'API: Delete Permission', 'permission:delete', 4, 8, NULL, NULL, 'DELETE /api/permission/{id}', 6, UTC_TIMESTAMP());

-- Level 4: API Permissions for User Management (type=4)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(50, 'API: Get Users', 'user:list', 4, 9, NULL, NULL, 'GET /api/user', 1, UTC_TIMESTAMP()),
(51, 'API: Get User', 'user:detail', 4, 9, NULL, NULL, 'GET /api/user/{id}', 2, UTC_TIMESTAMP()),
(52, 'API: Create User', 'user:create', 4, 9, NULL, NULL, 'POST /api/user', 3, UTC_TIMESTAMP()),
(53, 'API: Update User', 'user:update', 4, 9, NULL, NULL, 'PUT /api/user/{id}', 4, UTC_TIMESTAMP()),
(54, 'API: Delete User', 'user:delete', 4, 9, NULL, NULL, 'DELETE /api/user/{id}', 5, UTC_TIMESTAMP());

-- Level 4: API Permissions for Logs (type=4)
INSERT INTO `permissions` (`Id`, `Name`, `Code`, `Type`, `ParentId`, `Path`, `Icon`, `ApiPath`, `Sort`, `CreatedAt`) VALUES
(55, 'API: Get Logs', 'logs:list', 4, 10, NULL, NULL, 'GET /api/log', 1, UTC_TIMESTAMP()),
(56, 'API: Get Log', 'logs:detail', 4, 10, NULL, NULL, 'GET /api/log/{id}', 2, UTC_TIMESTAMP());

-- ============================================
-- 3. Default Role: Super Admin
-- ============================================

INSERT INTO `roles` (`Id`, `Name`, `Description`, `CreatedAt`)
VALUES (
    1,
    'Super Admin',
    'System administrator with all permissions',
    UTC_TIMESTAMP()
);

-- ============================================
-- 4. Link Super Admin Role to All Permissions
-- ============================================

-- Insert all permission IDs (1-56) for Super Admin role (Id=1)
INSERT INTO `role_permissions` (`RoleId`, `PermissionId`)
SELECT 1, Id FROM `permissions`;

-- ============================================
-- 5. Link admin User to Super Admin Role
-- ============================================

INSERT INTO `admin_roles` (`AdminId`, `RoleId`)
SELECT Id, 1 FROM `admin_users` WHERE `Username` = 'admin';

-- ============================================
-- Seed Data Summary
-- ============================================
-- Admin Users: 1 (admin / Admin@123)
-- Roles: 1 (Super Admin)
-- Permissions: 56 (4 Directories, 6 Menus, 20 Buttons, 26 APIs)
-- Role Permissions: 56 (Super Admin has all permissions)
-- Admin Roles: 1 (admin user is Super Admin)
-- ============================================
