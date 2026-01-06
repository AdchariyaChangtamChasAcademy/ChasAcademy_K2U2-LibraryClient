-- Checks if the database 'LibraryDB' exists
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    RAISERROR('LibraryDB does not exist', 16, 1);
    RETURN;
END;
GO

-- If 'LibraryDB' exists, switch to LibraryDB database (Just in case)
USE LibraryDB;
GO

-- If table 'Members' doesn't exist, create it
IF OBJECT_ID('dbo.Members', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Members(
		MemberID INT IDENTITY(1,1) PRIMARY KEY,
		FirstName NVARCHAR(100) NOT NULL,
		LastName NVARCHAR(100) NOT NULL,
		Email NVARCHAR(100) NOT NULL UNIQUE,
		Phone NVARCHAR(100),
		RegistrationDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE)
	);
END
GO

-- If table 'Books' doesn't exist, create it
IF OBJECT_ID('dbo.Books', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Books(
		BookID INT IDENTITY(1,1) PRIMARY KEY,
		Title NVARCHAR(100) NOT NULL,
		Author NVARCHAR(100) NOT NULL,
		ISBN VARCHAR(13) NOT NULL UNIQUE,
		PublicationDate DATE NOT NULL,
		Quantity INT NOT NULL CHECK (Quantity >= 0)
	);
END
GO

-- If table 'Loans' doesn't exist, create it
IF OBJECT_ID('dbo.Loans', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Loans(
		LoanID INT IDENTITY(1,1) PRIMARY KEY,
		LoanDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
		DueDate DATE NOT NULL,
		IsReturned BIT NOT NULL DEFAULT 0,
		FKMemberID INT NOT NULL FOREIGN KEY REFERENCES dbo.Members(MemberID),
		FKBookID INT NOT NULL FOREIGN KEY REFERENCES dbo.Books(BookID)
	);
END
GO

-- If table 'LoanReturns' doesn't exist, create it
IF OBJECT_ID('dbo.LoanReturns', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.LoanReturns(
		LoanReturnID INT IDENTITY(1,1) PRIMARY KEY,
		ReturnDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
		IsLate BIT,
		FKLoanID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES dbo.Loans(LoanID)
	);
END
GO