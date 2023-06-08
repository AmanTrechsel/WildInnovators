<?php
    session_start();
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
                        <li><a href="index.html">Home</a></li>
                        <li><a href="handleiding.html">Handleiding</a></li>
                        <li><a id="NavText" href="dashboard.html">Dashboard</a></li>
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
            <h1>Welkom <?php echo $_SESSION['username']; ?></h1>
        </div>
        <div id="quotes">
            <script src="scripts/quotes.js"></script>
        </div>
        <div id="quote">
            <img src="./images/balk.png" alt="quote">
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