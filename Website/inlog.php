<?php
    session_start(); //Session started on every page, in case someone is logged in
    require_once 'constants.php'; //Add the page for the database login constants 

    $errors = []; //Making a variable with an array in it for possible errors.

    if ($_SERVER['REQUEST_METHOD'] == "POST") //Check if the submit button is pushed
    {
        // Create variables for input and filter the input
        $username = filter_input(INPUT_POST, "username");
        $password = filter_input(INPUT_POST, "password");
        $hash = password_hash($password, PASSWORD_BCRYPT);

        try
        {
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}"); //Makes a connection with the database

            try
            {
                if (isset($_GET['register'])) //If the register button is pressed then do the following:
                {
                    $stmt = $dbHandler->prepare("INSERT INTO `accounts` (`name`, `password`) VALUES (:username, :password);"); //Prepare to insert the information/data into the database
                    $stmt->bindParam("username", $username, PDO::PARAM_STR); //Bind the variables so the info can get put into database
                    $stmt->bindParam("password", $hash, PDO::PARAM_STR);
                    $stmt->execute();

                    $_SESSION['username'] = $username; //Bind the session username to the username variable, if the user goes to a different page then he/she is still logged in. 
                    header("Location: dashboard.php"); //Set the location to the dashboard page. 
                }
                else //If the register button isnt pressed then do this:
                {
                    $stmt = $dbHandler->prepare("SELECT * FROM `accounts` WHERE `name` = :username"); //Look if the username is in the database
                    $stmt->bindParam("username", $username, PDO::PARAM_STR);
                    $stmt->bindColumn("password", $hashedPassword, PDO::PARAM_STR);
                    $stmt->execute();
                    $stmt->fetch(PDO::FETCH_ASSOC);

                    if ($username && $password && $hashedPassword && password_verify($password, $hashedPassword)) //Check if the database has the account and the password 
                    {
                      $_SESSION['username'] = $username;
                      header("Location: dashboard.php"); //If everything is correct then you'll get send to the dashboard page.
                    }
                    else
                    {
                      $errors[] = "Username or password is incorrect!"; //if the username or password is not correct then the message will go into the errors array, which will display later on in the page
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
                    <li><a href="index.php">Home</a></li>
                    <li><a href="handleiding.php">Handleiding</a></li>
                    <li><a id="NavText" href="#">Login</a></li>
                </ul>
            </nav>
        </header>
        <div id="content">
            <?php
                foreach ($errors as $error)
                {
                    echo "<p>".$error."</p>"; //Display all errors if there is any to display
                }
                if (isset($_GET['register'])) //If the user clicked on the register button than display this code underneath.
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
                <div id="loginbutton">
                    <input class="button" type="submit" value="Registreer">
                </div>  
            </form>
            <?php
                }
                else //If nothing is pressed than it will display the login form. 
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
                    <a href="#" id="forgotpass">Wachtwoord vergeten?</a>
                </div>
                <div class="loginbuttons">
                    <input class="button" type="submit" value="Login">   

                    </form>
                    <form  id="login" method="GET" action="#register">
                        <input class="button" type="submit" name="register" value="Registreren">                    
                    </form>
                </div>
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
                    <li><a href="policy.php">Policy</a></li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>
