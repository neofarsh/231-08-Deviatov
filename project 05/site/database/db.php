<?php
session_start();

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cwp1_231";

$conn = mysqli_connect($servername, $username, $password, $dbname);

if(!$conn){
    die("error". mysqli_connect_error());
} else {
    "connect";
} ?>