<?php
session_start();
?>
<!DOCTYPE html>
<html lang="ru">
<head>
    <title>Гойдаграм</title>
    <link rel="stylesheet" href="css/style.css">
    <link rel='stylesheet' href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css'>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@8/swiper-bundle.min.css"/>
    <script src="https://cdn.jsdelivr.net/npm/swiper@8/swiper-bundle.min.js"></script>
</head>
<body>


<!--
    <div class="preloader_scene">
        <div class="loading_block">
            <div class="title_loading">

            </div>
            <div class="progress">
                
            </div>
        </div>
    </div>

    <div class="preloader_scene">
        <div class="loading_block">
            <div class="title_loading">
                Подождите..
            </div>
            <div class="progress">
                
            </div>
        </div>
        <div class="preloader_block"></div>
        <div class="preloader_block"></div>
    </div>
-->

    
<div class="title">
    <h1>Рекомендации</h1>
</div>

<section class="homepage">
    <div class="sidebar">
        <?php if (!isset($_SESSION['user'])): ?>
            <a href="index/auth.php" class="icon"><i class='bx bx-user-circle'></i></a>
        <?php else: ?>
            <a href="index/profile.php" class="icon"><img class="avatar" src="images/avatar.jpeg" alt="" width="50" height="50"></a>
        <?php endif; ?>
        <a href="index.php" class="icon active"><i class='bx bxs-news'></i></a>
        <a href="#" class="icon inactive"><i class='bx bxs-comment-dots'></i></a>    
        <a href="#" class="icon inactive"><i class='bx bxs-user-pin'></i></a>  
        <a href="index/about.php" class="icon icon-bottom"><i class='bx bx-info-circle'></i></a> 
    </div>
</section>

<section class="home">
    <div class="container swiper">
        <div class="wrapper swiper-wrapper">
            <div class="slide swiper-slide">
                <div class="content one">
                    <span>Добро пожаловать!</span>
                    <p>Самая удобная социальная сеть с привычным функционалом.</p>
                    <button class="btn-register" onclick="window.location.href='index/auth.php';">Регистрация</button>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content two">
                    <span>Действия</span>
                    <p>Общайтесь, читаете, взаимодействуйте.</p>
                    <label>Гойдаграм абсолютно открыт для вашего использования!</label>
                </div>
            </div>  
            <div class="slide swiper-slide">
                <div class="content three">                    
                    <span>Общайтесь</span>
                    <p>Пригласите своих друзей и создавайте группы до 100 человек.</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content four">                    
                    <span>Читайте</span>
                    <p>Гойдаграм поддерживает одну ленту новостей с вашими сообществами.</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content five">                    
                    <span>Взаимодействуйте</span>
                    <p>Вы уже знаете что нужно делать!</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content six">                    
                    <span>Будьте уверены</span>
                    <p>Это же вообще не соц. сеть, а выдумка, нафига я это все пишу</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content seven">                    
                    <span>Творчество</span>
                    <p>Мне что серьезно надо запихнуть сюда 10 демотиваторских рекламных карточек социальной сети.</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content eight">                    
                    <span>Лента новостей</span>
                    <p>Умная система рекомендации подберет только нужный и актуальный контент</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content nine">                    
                    <span>Вы у цели!</span>
                    <p>Зарегестрируйся, это просто.</p>
                </div>
            </div>
            <div class="slide swiper-slide">
                <div class="content ten">                    
                    <span>Всё готово.</span>
                    <p>Подписывайтесь на сообщества, читайте каналы друг-дргуга и пользуйтесь социальной сетью чтобы она смогла подобрать ваши рекомендации.</p>
                    <button class="btn-register" onclick="window.location.href='index/auth.php';">Войти</button>
                </div>
            </div>
        </div>
        <div class="swiper-button-next"></div>
        <div class="swiper-button-prev"></div>
        <div class="swiper-pagination"></div>
    </div>
    <div class="heading news">
        <p>Новостей пока нет...</p>
    </div>
</section>



<script>
    var swiper = new Swiper(".container", {
      spaceBetween: 30,
      centeredSlides: true,
      autoplay: {
        delay: 2000,
        disableOnInteraction: false,
      },
      pagination: {
        el: ".swiper-pagination",
        clickable: true,
      },
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
      loop: true,
    });
</script>

</body>
</html>
