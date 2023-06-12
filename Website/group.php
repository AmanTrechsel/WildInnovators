<?php
    session_start();
    require_once 'constants.php';

    $errors = [];

    function generateRandomString($length = 6) {
        $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        $charactersLength = strlen($characters);
        $randomString = '';
        for ($i = 0; $i < $length; $i++) {
            $randomString .= $characters[random_int(0, $charactersLength - 1)];
        }
        return $randomString;
    }

    $groupCode = generateRandomString();

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        // Check sign in
        $groupName = filter_input(INPUT_POST, "groupName");
        $subjects = filter_input(INPUT_POST, 'subject', FILTER_DEFAULT, FILTER_REQUIRE_ARRAY);
        
        $subject = implode(", ", $subjects);
        try
        {
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");

            try
            {
                $stmt = $dbHandler->prepare("INSERT INTO  `groups` (`name`, `code`, `subjects`, `username`) VALUES (:groupName, :groupCode, :subjects, :userName);");
                $stmt->bindParam("groupName", $groupName, PDO::PARAM_STR);
                $stmt->bindParam("groupCode", $groupCode, PDO::PARAM_STR);
                $stmt->bindParam("subjects", $subject, PDO::PARAM_STR);
                $stmt->bindParam("userName", $username, PDO::PARAM_STR);
                $stmt->execute();
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
                    <li><a href="#">Home</a></li>
                    <li><a href="handleiding.php">Handleiding</a></li>
                    <li><a id="NavText" href="dashboard.php">Dashboard</a></li>
                </ul>
            </nav>
        </header>
        <main>
        </main>
        <div id="createGroup">
            <div class="loginprompt">
                <h1>Groep creëren</h1>
            </div>
            <form method="POST" action="#">
            <div class="loginprompt">
                <h4>Groepsnaam</h4>
            </div>
            <div class="loginprompt">
                <input type="text" id="groupName" name="groupName" placeholder="Mijn groepsnaam..." required>
            </div>
            <div class="loginprompt">
                <h4>Onderdelen</h4>
            </div>
            <div class="loginprompt">
                <div>
                    <input type="checkbox" id="biology" name="subject[]" value="biology">
                    <label for="biology">Biologie</label></p>
                    <input type="checkbox" id="geography" name="subject[]" value="geography">
                    <label for="geography">Aardrijkskunde</label></p>
                    <input type="checkbox" id="history" name="subject[]" value="history">
                    <label for="history">Geschiedenis</label></p>
                </div>
            </div>
            <div class="loginprompt">
                <input type="submit" id="createButton" name="createGroup" value="Creëer">
            </div>
            </form>
        </div>
       
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
