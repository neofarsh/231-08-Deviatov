<?php
session_start();
?>
<!DOCTYPE html>
<html lang="ru">
<head>
    <title>Гойдаграм</title>
    <link rel="stylesheet" href="../css/style.css">
    <link rel='stylesheet' href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css'>

</head>

<section class="homepage">
    <div class="sidebar">
        <?php if (!isset($_SESSION['user'])): ?>
            <a href="auth.php" class="icon active"><i class='bx bx-user-circle'></i></a>
        <?php else: ?>
            <a href="profile.php" class="icon"><img class="avatar" src="../images/avatar.jpeg" alt="" width="50" height="50"></a>
        <?php endif; ?>
        <a href="../index.php" class="icon"><i class='bx bxs-news'></i></a>
        <a href="#" class="icon inactive"><i class='bx bxs-comment-dots'></i></a>    
        <a href="#" class="icon inactive"><i class='bx bxs-user-pin'></i></a>    
        <a href="about.php" class="icon icon-bottom"><i class='bx bx-info-circle' ></i></a> 
    </div>
</section>

<div class="title">
    <h1>Профиль</h1>
</div>
<div class="heading command">
    <a href="../database/logout.php">Выйти из аккаунта</a>
</div>
</html>