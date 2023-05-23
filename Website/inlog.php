<?php
    session_start();
    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        // Check sign in
    }
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./style/style.css">
    <title>Wild Innovators</title>
</head>
<body>
    <div id="mainContainer">
        <header>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a id="HomeText" href="index.html">Home</a></li>
                    <li><a href="handleiding.html">Handleiding</a></li>
                    <li><a href="#">Login</a></li>
                </ul>
            </nav>
        </header>
        <div id="content">
            <?php
                if (isset($_GET['register']))
                {
            ?>
            <h1>Registreren</h1>
            <form method="POST" action="#">
                <label for="username">Gebruikersnaam</label>
                <input type="text" id="username" name="username" placeholder="Gebruikersnaam..." required>
                <label for="password">Wachtwoord</label>
                <input type="text" id="password" name="password" placeholder="Wachtwoord..." required>
                <input type="submit" value="Login">
            </form>
            <?php
                }
                else
                {
            ?>
            <form method="POST" action="#">
                <label for="username">Gebruikersnaam</label>
                <input type="text" id="username" name="username" placeholder="Gebruikersnaam..." required>
                <label for="password">Wachtwoord</label>
                <input type="text" id="password" name="password" placeholder="Wachtwoord..." required>
                <a href="#" id="forgotpass">Wachtwoord vergeten?</a>
                <input type="submit" value="Login">
            </form>
            <form method="GET" action="#register">
                <input type="submit" name="register" value="Registreren">
            </form>
            <?php
                }
            ?>
        </div>
    </div>
</body>
</html>
