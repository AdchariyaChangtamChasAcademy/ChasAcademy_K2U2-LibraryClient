# READ ME
## Summary
This project implements a library system for managing books, members, loans, and returns. The system uses SQL Server for data storage and Entity Framework in C# to interact with the database through a console application.

## How to
### 1. Create Database, LibraryDB
1. Navigate to the `Database` folder.
2. Execute the SQL scripts in SQL Server Management Studio (SSMS) in numerical order to create the database, tables, and other objects.
3. *(Optional)* Navigate to the `Data` folder and run `01_InsertTestData.sql` to insert sample data into the database.

### 2. Run Console App
1. Open the project in Visual Studio.
2. Ensure the project targets .NET 8.0.
3. Run the program. (Ctrl + F5)
4. Follow the console prompts.

## Entity-Relationship Diagram
<img width="1773" height="869" alt="ERD_Library" src="https://github.com/user-attachments/assets/61700d71-a8e3-4b03-803d-5a9ab84ade1a" />

## Reflections
Dataintegrity: 
The use of primary keys, foreign keys, constraints, triggers, and stored procedures ensures that all relationships and database updates are accurate. This guarantees correct loan records, avoids duplicate entries, and eliminates redundant data.

Optimization:
Placing indexes on foreign keys and on frequently searched columns reduces full table scans and improves JOIN performance. Examples include indexes on Loans.FKMemberID, Loans.FKBookID, and LoanReturns.FKLoanID.

Normalization (3NF):
The tables follow Third Normal Form (3NF), which eliminates redundancy and transitive dependencies. All non-key attributes depend on the entire primary key.

## Execution plan example
### Index show case query
```sql
SELECT *
FROM Loans l
LEFT JOIN LoanReturns r ON l.LoanID = r.FKLoanID
WHERE r.FKLoanID IS NULL;
```
### Index show case execution plan
<img width="1253" height="644" alt="image" src="https://github.com/user-attachments/assets/0016ec39-359e-4598-be9f-6fc896aed812" />
