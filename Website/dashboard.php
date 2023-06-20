<?php
    session_start(); //Start the session
    require_once 'constants.php'; //Add the page for the login constants
    $username = $_SESSION['username']; //Get the variable out of the session
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
        <header id="dashboardHeader">
                <img src="./images/LogoGroepjeWhite.png">
                <nav>
                    <ul>
                        <li><a href="index.php">Home</a></li>
                        <li><a href="handleiding.php">Handleiding</a></li>
                        <li><a id="NavText" href="dashboard.php">Dashboard</a></li>
                    </ul>
                </nav>
        </header>
        <main>
            <div id="group">
                <img class="buttonLogo" src="images/Group.png">
                <form  id="login" method="GET" action="group.php">
                    <input class="buttonDashboard" type="submit" name="register" value="Creëer een groep">                    
                </form>
            </div>
            <div id="les">
                <img class="buttonLogo" src="images/Create.png">
                <form  id="login" method="GET" action="lesson.php">
                    <input class="buttonDashboard" type="submit" name="register" value="Creëer je eigen les">                    
                </form>
            </div>
        </main>
        <div id="welkom">
            <h1>Welkom <?php echo $_SESSION['username']; ?></h1> <!--Show the username for a little personal touch-->
        </div>
        <div id="quoteContainer">
            <script src="scripts/quotes.js"></script>
        </div>
        <div id="quoteDashboard">
            <img src="./images/balk.png" alt="quote">
        </div>
        <div id="groups">
            <h1>Mijn Groepen</h1>
            <?php
                try {
                    $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");
                    
                
                    $stmt = $dbHandler->prepare("SELECT `name`, `code` FROM `groups` WHERE `username` = :username");
                    $stmt->bindParam(":username", $username, PDO::PARAM_STR); //Bind the variables
                    $stmt->execute();
                    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
                
                    foreach ($results as $result) { //Everything out of the table goes in to the groupview to show the different groups and code
                        echo "<div class='groupView'>
                                <p>Groep: $result[name]</p>
                                <p>Code: $result[code] </p> 
                            </div>";
                    }
                } catch (PDOException $ex) {
                    $errors[] = $ex->getMessage(); //If there is any erros put it in the error array
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