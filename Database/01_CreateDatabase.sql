-- Creates the database 'LibraryDB'
-- Run in master

IF DB_ID('LibraryDB') IS NULL
BEGIN
    CREATE DATABASE LibraryDB;
END;
GO

USE LibraryDB;
GO