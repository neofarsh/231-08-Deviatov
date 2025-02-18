<?php
require_once('db.php');
session_start();

$number = $_POST['number'];
$pass = $_POST['pass'];

$sql = "SELECT * FROM `devyatov_231` WHERE number = ?";
$stmt = $conn->prepare($sql);
$stmt->bind_param("s", $number);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $row = $result->fetch_assoc();
    
    if (password_verify($pass, $row['pass'])) {
        $_SESSION['user'] = $row['number'];
        header('Location: ../index.php');
        exit();
    }
}
header('Location: ../index/auth.php?error=1');
exit();
?>