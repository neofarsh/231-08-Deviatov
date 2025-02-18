<?php
session_start();
?>
<!DOCTYPE html>
<html lang="ru">
<head>
    <title>Гойдаграм</title>
    <link rel="stylesheet" href="../css/style.css">
    <link rel='stylesheet' href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css'>
    <link rel="icon" href="../images/icon_main.png" type="image/png">
</head>
<body>

<div class="title">
    <h1>Авторизация</h1>
</div>

<section class="homepage">
    <div class="sidebar">
        <?php if (!isset($_SESSION['user'])): ?>
            <a href="auth.php" class="icon active"><i class='bx bx-user-circle'></i></a>
        <?php else: ?>
            <a href="profile.php" class="icon"><img class="avatar" src="images/avatar.jpeg" alt="" width="50" height="50"></a>
        <?php endif; ?>
        <a href="../index.php" class="icon"><i class='bx bxs-news'></i></a>
        <a href="#" class="icon inactive"><i class='bx bxs-comment-dots'></i></a>    
        <a href="#" class="icon inactive"><i class='bx bxs-user-pin'></i></a>   
        <a href="about.php" class="icon icon-bottom"><i class='bx bx-info-circle' ></i></a> 
    </div>
</section>

<section class="auth">
    <div class="tabs">
        <?php if (isset($_GET['error']) && $_GET['error'] == 1): ?>
            <p style="color: red;">Неверный номер или пароль.</p>
        <?php endif; ?>
        <?php if (isset($_GET['error']) && $_GET['error'] == 2): ?>
            <p style="color: red;">Номер уже зарегистрирован.</p>
        <?php endif; ?>
        <?php if (isset($_GET['error']) && $_GET['error'] == 3): ?>
            <p style="color: red;">Пароли не совпадают.</p>
        <?php endif; ?>
        <input class="radio-button" id="tab-1" name="tab-btn" type="radio" checked> <label for="tab-1">регистрация</label> <label for="">|</label>
        <input class="radio-button" id="tab-2" name="tab-btn" type="radio"> <label for="tab-2">войтиㅤ</label>
       
        <div class="tab-content" id="content-1">
            <form action="../database/register.php" method="post">
                <input class="input-field" type="tel" pattern="^[78]\d{10}$" placeholder="номер телефона" name="number" required>    
                <input class="input-field" type="password" placeholder="пароль" name="pass" minlength="8" required>
                <input class="input-field" type="password" placeholder="повторите пароль" name="repeatpass" minlength="8" required>
                <label class="checkbox-field" for="polit_box"> <input type="checkbox" id="polit_box" required> Согласен с <a href="../files/rules.pdf" target="_blank">политикой..</a></label>
                <button class="btn-register" type="submit">Регистрация</button>
            </form>
        </div>

        <div class="tab-content" id="content-2">
            <form action="../database/login.php" method="post">
                <input class="input-field" type="tel" pattern="^[78]\d{10}$" placeholder="номер телефона" name="number" required>    
                <input class="input-field" type="password" placeholder="пароль" name="pass" required>
                <button type="submit">Войти</button>
                <label class="checkbox-field" for="polit_box"><a href="https://t.me/dcdae" target="_blank">Забыли пароль?</a></label>
            </form>
        </div>
    </div>
</section>

</html>     