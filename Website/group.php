<?php
    session_start();
    require_once 'constants.php';

    $errors = [];

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        // Check sign in
        $groupsName = filter_input(INPUT_POST, "groupsName");
        

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
                    header("Location: dashboard.php");
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
                      header("Location: dashboard.php");
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
                    <li><a id="NavText" href="#">Home</a></li>
                    <li><a href="handleiding.html">Handleiding</a></li>
                    <li><a href="inlog.php">Login</a></li>
                </ul>
            </nav>
        </header>
        <main>
            <div id="createGroup">
                <div class="loginprompt">
                    <h1>Group creëren</h1>
                </div>
                    <form method="POST" action="dashboard.php">
                <div class="loginprompt">
                    <h4>Groepsnaam</h4>
                </div>
                <div class="loginprompt">
                    <p><input type="text" name="groupsName" placeholder="Mijn groepsnaam..."></p>
                </div>
                <div class="loginprompt">
                    <h4>Onderdelen</h4>
                </div>
                <div class="loginprompt">
                    <p><input type="checkbox" id="lesson1" name="lesson1" value="lesson1">
                    <label for="lesson1">Placeholder</label></p>
                    <p><input type="checkbox" id="lesson2" name="lesson2" value="lesson2">
                    <label for="lesson2">Placeholder</label></p>
                    <p><input type="checkbox" id="lesson3" name="lesson3" value="lesson3">
                    <label for="lesson3">Placholder</label></p>
                </div>
                <div class="loginprompt">
                    <input type="submit" name="createGroup" value="Creëer">
                </div>
                </form>
            </div>
        </main>
        <footer>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li>Copyright</li>
                    <li>Contact</li>
                    <li><a href="policy.html">Policy</a></li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>