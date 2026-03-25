-- ============================================
-- Shoppe Admin Database Initialization Script
-- ============================================
-- Database: shoppe_admin
-- Character Set: utf8mb4
-- Collation: utf8mb4_unicode_ci
-- ============================================

-- Create database if not exists
CREATE DATABASE IF NOT EXISTS shoppe_admin
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE shoppe_admin;

-- ============================================
-- Table Structures (EF Core will handle creation via migrations)
-- This script is provided as a reference/fallback
-- ============================================

-- admin_users table
CREATE TABLE IF NOT EXISTS `admin_users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` varchar(50) NOT NULL,
    `PasswordHash` varchar(255) NOT NULL,
    `Email` varchar(100) DEFAULT NULL,
    `Phone` varchar(20) DEFAULT NULL,
    `Status` int NOT NULL DEFAULT 1,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `LastLoginAt` datetime(6) DEFAULT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_admin_users_Username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- roles table
CREATE TABLE IF NOT EXISTS `roles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(50) NOT NULL,
    `Description` varchar(255) DEFAULT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- permissions table
CREATE TABLE IF NOT EXISTS `permissions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) NOT NULL,
    `Code` varchar(100) NOT NULL,
    `Type` int NOT NULL,
    `ParentId` int DEFAULT NULL,
    `Path` varchar(255) DEFAULT NULL,
    `Icon` varchar(100) DEFAULT NULL,
    `ApiPath` varchar(255) DEFAULT NULL,
    `Sort` int NOT NULL DEFAULT 0,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    PRIMARY KEY (`Id`),
    KEY `IX_permissions_ParentId` (`ParentId`),
    CONSTRAINT `FK_permissions_permissions_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `permissions` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- admin_roles table (many-to-many)
CREATE TABLE IF NOT EXISTS `admin_roles` (
    `AdminId` int NOT NULL,
    `RoleId` int NOT NULL,
    PRIMARY KEY (`AdminId`, `RoleId`),
    KEY `IX_admin_roles_RoleId` (`RoleId`),
    CONSTRAINT `FK_admin_roles_admin_users_AdminId` FOREIGN KEY (`AdminId`) REFERENCES `admin_users` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_admin_roles_roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- role_permissions table (many-to-many)
CREATE TABLE IF NOT EXISTS `role_permissions` (
    `RoleId` int NOT NULL,
    `PermissionId` int NOT NULL,
    PRIMARY KEY (`RoleId`, `PermissionId`),
    KEY `IX_role_permissions_PermissionId` (`PermissionId`),
    CONSTRAINT `FK_role_permissions_permissions_PermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `permissions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_role_permissions_roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- operation_logs table
CREATE TABLE IF NOT EXISTS `operation_logs` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `AdminId` int NOT NULL,
    `Action` varchar(50) NOT NULL,
    `Module` varchar(50) NOT NULL,
    `Details` varchar(4000) DEFAULT NULL,
    `IpAddress` varchar(50) DEFAULT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    PRIMARY KEY (`Id`),
    KEY `IX_operation_logs_AdminId` (`AdminId`),
    KEY `IX_operation_logs_CreatedAt` (`CreatedAt`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
