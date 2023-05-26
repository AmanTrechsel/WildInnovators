<?php
    session_start();
    require_once 'constants.php';

    $errors = [];

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        // Check sign in
        $username = filter_input(INPUT_POST, "username");
        $password = filter_input(INPUT_POST, "password");

        try
        {
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");

            try
            {
                if (isset($_GET['register']))
                {
                    $stmt = $dbHandler->prepare("INSERT INTO `users` (`username`, `password`) VALUES (:username, :password);");
                    $stmt->bindParam("username", $username, PDO::PARAM_STR);
                    $stmt->bindParam("password", password_hash($password, PASSWORD_BCRYPT), PDO::PARAM_STR);
                    $stmt->execute();

                    $_SESSION['username'] = $username;
                    header("Location: index.php");
                }
                else
                {
                    $stmt = $dbHandler->prepare("SELECT * FROM `users` WHERE `username` = :username");
                    $stmt->bindParam("username", $username, PDO::PARAM_STR);
                    $stmt->bindColumn("password", $hashedPassword, PDO::PARAM_STR);
                    $stmt->execute();
                    $stmt->fetch(PDO::FETCH_ASSOC);

                    if ($username && $password && $hashedPassword && password_verify($password, $hashedPassword))
                    {
                      $_SESSION['username'] = $username;
                      header("Location: index.php");
                    }
                    else
                    {
                      $errors[] = "Username or password is incorrect!";
                    }
                }
            }
            catch (Exception $ex)
            {
                $errors[] = $ex->getMessage();
            }
        }
        catch (Exception $ex)
        {
            $errors[] = $ex->getMessage();
        }
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
                    <li><a href="index.html">Home</a></li>
                    <li><a href="handleiding.html">Handleiding</a></li>
                    <li><a id="NavText" href="#">Login</a></li>
                </ul>
            </nav>
        </header>
        <div id="content">
            <?php
                foreach ($errors as $error)
                {
                    echo "<p>".$error."</p>";
                }
                if (isset($_GET['register']))
                {
            ?>
            <form id ="register" method="POST" action="#">
                <div class="loginprompt">
                    <h1>Registreren</h1>
                </div>
                <div class="loginprompt">
                    <label for="username">Gebruikersnaam</label>
                    <input type="text" id="username" name="username" placeholder="Mijn gebruikersnaam..." required>
                </div>
                <div class="loginprompt">
                    <label for="password">Wachtwoord</label>
                    <input type="password" id="password" name="password" placeholder="Mijn wachtwoord..." required>
                </div>
                <div class="loginprompt">
                    <input class="button" type="submit" value="Login">
                </div>  
            </form>
            <?php
                }
                else
                {
            ?>
            <form id ="login" method="POST" action="#">
                <div class="loginprompt">
                    <label for="username">Gebruikersnaam</label>
                    <input class="field" type="text" id="username" name="username" placeholder="Gebruikersnaam..." required>
                </div>
                <div class="loginprompt">
                    <label for="password">Wachtwoord</label>
                    <input class="field" type="password" id="password" name="password" placeholder="Wachtwoord..." required>
                </div>
                <div class="loginprompt">
                    <a href="#" id="forgotpass">Wachtwoord vergeten?</a>
                    <input class="button" type="submit" value="Login">   
                </div>
            </form>
            <form method="GET" action="#register">
                <input class="button" type="submit" name="register" value="Registreren">                    
            </form>
            <?php
                }
            ?>
        </div>
        <footer>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li>Copyright</li>
                    <li>Contact</li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>
