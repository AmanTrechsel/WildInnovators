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
            <h1>TimeWise: de kennis op je device</h1>
            <p>TimeWise een interactieve app voor in het onderwijs.</p>
        </main>
        <div id="timeWiseLogo">
            <img src="./images/AppLogoPNG.png" alt="TimeWise">  
        </div>
        <div id="aboutTimeWise">
            <div id="aboutTimeWisetext">
                <h1>Voor het onderwijs</h1>
                <p>TimeWise is een interactieve app voor in het onderwijs. Waarbij er gebruik wordt gemaakt van augumented reality.
                    Waardoor wij verschillende objecten in de echte wereld kunnen plaatsen, zodat leren effectiever, leuker en een leerzame proces word.</p>
            </div>
            <img src="./images/App1.png" alt="Voorbeeld App">
        </div>
        <div id="aboutUs">
            <div id="aboutUstext">
                <h1>Wij zijn Wild Innovators</h1>
                <p>Hallo, wij zijn eerste jaars ICT studenten aan het NHL-Stenden in Emmen. Ons is gevraagd om een innovatief product te realiseren.</p>
                <p>Ons product ‘TimeWise’ is een augumented reality app voor in het onderwijs.</p>
            </div>
            <img id="arrow" src="./images/Arrow.png" alt="Pijl">
            <img src="./images/GroupPhoto.png" alt="Groeps Foto">
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
