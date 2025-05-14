-- Library Management SQL Project

-- 1. Create tables
CREATE TABLE authors (
    author_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE books (
    book_id SERIAL PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    author_id INTEGER REFERENCES authors(author_id),
    genre VARCHAR(50),
    published_year INT,
    copies_available INT DEFAULT 1
);

CREATE TABLE members (
    member_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    join_date DATE DEFAULT CURRENT_DATE
);

CREATE TABLE loans (
    loan_id SERIAL PRIMARY KEY,
    book_id INTEGER REFERENCES books(book_id),
    member_id INTEGER REFERENCES members(member_id),
    loan_date DATE DEFAULT CURRENT_DATE,
    return_date DATE
);

-- 2. Insert sample data
INSERT INTO authors (name) VALUES
('George Orwell'),
('J.K. Rowling'),
('J.R.R. Tolkien');

INSERT INTO books (title, author_id, genre, published_year, copies_available) VALUES
('1984', 1, 'Dystopian', 1949, 3),
('Harry Potter and the Sorcerer''s Stone', 2, 'Fantasy', 1997, 5),
('The Hobbit', 3, 'Fantasy', 1937, 2);

INSERT INTO members (name, join_date) VALUES
('Alice Johnson', '2024-01-15'),
('Bob Smith', '2024-02-10');

INSERT INTO loans (book_id, member_id, loan_date, return_date) VALUES
(1, 1, '2024-04-01', NULL),
(2, 2, '2024-04-05', '2024-04-20');

-- 3. Example queries

-- a) List all books and their authors
SELECT b.title, a.name AS author, b.genre, b.published_year
FROM books b
JOIN authors a ON b.author_id = a.author_id;

-- b) Show all current loans (books not yet returned)
SELECT l.loan_id, b.title, m.name AS member, l.loan_date
FROM loans l
JOIN books b ON l.book_id = b.book_id
JOIN members m ON l.member_id = m.member_id
WHERE l.return_date IS NULL;

-- c) Count the number of books available by genre
SELECT genre, SUM(copies_available) AS total_copies
FROM books
GROUP BY genre;

-- d) Find members who have borrowed more than 1 book
SELECT m.name, COUNT(*) AS books_borrowed
FROM loans l
JOIN members m ON l.member_id = m.member_id
GROUP BY m.name
HAVING COUNT(*) > 1;

-- e) Update: Mark a book as returned
UPDATE loans SET return_date = CURRENT_DATE WHERE loan_id = 1;

-- f) Delete: Remove a member who left
DELETE FROM members WHERE name = 'Bob Smith';

