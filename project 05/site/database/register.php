<?php
require_once('db.php');
session_start();

$number = $_POST['number'];
$pass = $_POST['pass'];
$repeatpass = $_POST['repeatpass'];

if ($pass !== $repeatpass) {
    header('Location: ../index/auth.php?error=3');
    exit();
}

$sql_check = "SELECT * FROM `devyatov_231` WHERE number = ?";
$stmt_check = $conn->prepare($sql_check);
$stmt_check->bind_param("s", $number);
$stmt_check->execute();
$result_check = $stmt_check->get_result();

if ($result_check->num_rows > 0) {
    header('Location: ../index/auth.php?error=2');
    exit();
} else {
    $hashed_pass = password_hash($pass, PASSWORD_DEFAULT);
    $sql = "INSERT INTO `devyatov_231` (number, pass) VALUES (?, ?)";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ss", $number, $hashed_pass);
    
    if ($stmt->execute()) {
        $_SESSION['user'] = $number;
        header('Location: ../index.php');
        exit();
    } else {
        echo "Ошибка регистрации: " . $conn->error;
    }
}
?>