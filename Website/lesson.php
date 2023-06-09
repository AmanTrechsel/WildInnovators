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
                    <li><a href="inlog.php">Login</a></li>
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
