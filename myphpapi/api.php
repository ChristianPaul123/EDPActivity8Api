<?php
header("Content-Type: application/json");

$host = 'localhost';
$db = 'library';
$user = 'root';
$pass = '';
$charset = 'utf8mb4';

$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$options = [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES => false,
];

$pdo = new PDO($dsn, $user, $pass, $options);

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
        $stmt = $pdo->query("SELECT books.book_id, books.book_title, books.book_number, books.book_published, books.book_quantity, books.date_added, author.author_name, category.category_name
                             FROM books
                             JOIN author ON books.book_author = author.author_id
                             JOIN category ON books.book_category = category.category_id");
    $books = $stmt->fetchAll();
    echo json_encode($books);

} elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $input = json_decode(file_get_contents('php://input'), true);
    $sql = "INSERT INTO books (book_title, book_author, book_category, book_number, book_published, book_quantity, date_added)
            VALUES (?, ?, ?, ?, ?, ?, ?)";
    $stmt = $pdo->prepare($sql);
    $stmt->execute([
        $input['book_title'],
        $input['book_author'],
        $input['book_category'],
        $input['book_number'],
        $input['book_published'],
        $input['book_quantity'],
        $input['date_added']
    ]);
    echo json_encode(['message' => 'Book added successfully', 'book_id' => $pdo->lastInsertId()]);
}

