-- create schema creed;

-- create statements
/*CREATE TABLE Book (
	BookId int auto_increment PRIMARY KEY,
    Title varchar(200),
    SerialNumber varchar(200),
    Author varchar(200),
    Publisher varchar(200)
);*/

/*CREATE TABLE Customer (
	CustomerId int auto_increment PRIMARY KEY,
    Name varchar(200),
    Address varchar(200),
    Contact varchar(200)
)*/

/*CREATE TABLE BorrowHistory (
	BorrowHistoryId int auto_increment PRIMARY KEY,
    BookId int NOT NULL,
    CustomerId int NOT NULL,
    BorrowDate DATETIME NOT NULL,
    ReturnDate DATETIME,
    FOREIGN KEY(BookId) REFERENCES book(BookId),
    FOREIGN KEY(CustomerId) REFERENCES customer(CustomerId)
)*/

-- Select * from book;

-- select * from customer;

-- select * from borrowhistory

-- Insert statements
/*INSERT INTO book(Title,SerialNumber,Author,Publisher) VALUES
('Book1','ABCD1','Auth1','Pub1'),
('Book2','ABCD2','Auth1','Pub1'),
('Book3','ABCD3','Auth1','Pub1'),
('Book4','ABCD4','Auth1','Pub1'),
('Book5','ABCD5','Auth1','Pub1');

INSERT INTO customer(Name,Address,Contact) VALUES
('Customer1','Add','Cont'),
('Customer2','Add','Cont'),
('Customer3','Add','Cont'),
('Customer4','Add','Cont'),
('Customer5','Add','Cont');*/

/*DELIMITER //
CREATE PROCEDURE GetAllBooks()
BEGIN
	Select * from book;
END //

DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetBook(book_id INT)
BEGIN
	Select * from book where BookId = book_id;
END //

DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetAllCustomers()
BEGIN
	Select * from customer;
END //

DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetCustomer(customer_id INT)
BEGIN
	Select * from customers where CustomerId = customer_id;
END //

DELIMITER ;
*/

/*DELIMITER //
CREATE PROCEDURE GetAllBorrowHistories()
BEGIN
	select BorrowHistoryId, book.BookId, book.Title,
    customer.CustomerId,
    customer.Name,
    BorrowDate, ReturnDate 
    from borrowhistory
    inner join book
    on book.BookId = borrowhistory.BookId
    inner join customer
    on customer.CustomerId = borrowhistory.CustomerId;
END //

DELIMITER ;*/

/*DELIMITER //
CREATE PROCEDURE BorrowBook(book_id INT, customer_id INT, borrow_date DATETIME)
BEGIN
	Insert into borrowhistory(BookId, CustomerId, BorrowDate)
    VALUES (book_id, customer_id, borrow_date);
END //

DELIMITER ;*/

DELIMITER //
CREATE PROCEDURE ReturnBook(borrow_history_id INT)
BEGIN
	Update borrowhistory
    set ReturnDate = NOW()
    where BorrowHistoryId = borrow_history_id;
END //

DELIMITER ;


