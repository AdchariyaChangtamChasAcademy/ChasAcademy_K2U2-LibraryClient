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

-- Inserts test data into each tables
INSERT INTO Members (FirstName, LastName, Email, Phone, RegistrationDate)
VALUES
('Alice', 'Wonderland', 'alice.wonderland@email.com', '070-1234567', '2025-01-01'),
('Bob', 'Marley', 'bob.marley@email.com', NULL, '2025-02-02'),
('Charlie', 'Brown', 'charlie.brown@email.com', '073-7654321', '2025-03-03');
GO

INSERT INTO Books (Title, Author, ISBN, PublicationDate, Quantity)
VALUES
('The Hobbit', 'J.R.R. Tolkien', '9780547928227', '1937-11-21', 2),
('To Kill a Mockingbird', 'Harper Lee', '9780061120084', '1960-07-11', 3),
('Nineteen Eighty-Four', 'George Orwell', '9780201633610', '1949-06-08', 4);
GO

INSERT INTO Loans(LoanDate, DueDate, FKMemberID, FKBookID)
VALUES
('2025-01-01', '2025-01-15', 1, 2),
('2025-02-02', '2025-02-16', 2, 3),
('2026-01-01', '2026-01-15', 3, 1);
GO

INSERT INTO LoanReturns(ReturnDate, IsLate, FKLoanID)
VALUES
('2025-01-04', 0, 1),
('2025-02-20', 1, 2),
('2026-01-15', 0, 3);
GO

-- View test data in each tables
SELECT * FROM Members;
SELECT * FROM Books;
SELECT * FROM Loans;
SELECT * FROM LoanReturns;

-- FOR TESTING PURPOSES
--TRUNCATE TABLE Members;
--TRUNCATE TABLE Books;
--TRUNCATE TABLE Loans;
--TRUNCATE TABLE LoanReturns;