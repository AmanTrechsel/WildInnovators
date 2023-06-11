<?php
    session_start();
    require_once 'constants.php';

    $errors = [];

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        // Check sign in

        try
        {
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");

            try
            {
                $stmt = $dbHandler->prepare("INSERT INTO  `` (``, ``, ``) VALUES (:, :, :);");
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
                    <li><a id="NavText" href="#">Home</a></li>
                    <li><a href="handleiding.php">Handleiding</a></li>
                    <li><a id="NavText" href="dashboard.php">dashboard</a></li>
                </ul>
            </nav>
        </header>
        <main>
        </main>
        <div id="createGroup">
            <div class="loginprompt">
                <h1>Les creëren</h1>
            </div>
            <form method="POST" action="#">
            <div class="loginprompt">
                <h4>Lesnaam</h4>
            </div>
            <div class="loginprompt">
                <input type="text" id="lessonName" name="lessonName" placeholder="Mijn lesnaam..." required>
            </div>
            <div class="loginprompt">
                <h4>Selecteer een groep</h4>
            </div>
            <div class="loginprompt">
                <div>
                    <label for="groups">Kies een groep:</label>
                    <select name="groups" id="groups">
                        <?php
                            $stmt = $dbHandler->prepare("SELECT * FROM `groups` WHERE `username` = :username");
                            $stmt->bindParam("username", $username, PDO::PARAM_STR);
                            $stmt->bindColumn("password", $hashedPassword, PDO::PARAM_STR);
                            $stmt->execute();
                            $stmt->fetchAll(PDO::FETCH_ASSOC);

                            foreach(){
                                echo '<option value=""></option>'
                            }
                        ?>
                    </select>
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
