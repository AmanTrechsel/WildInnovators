<?php
    session_start();
    require_once 'constants.php';

    $username = $_SESSION['username'];

    $errors = [];

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        $groupID = filter_input(INPUT_POST, "groupID");
        $lessonName = filter_input(INPUT_POST, "lessonName");
        $subject = filter_input(INPUT_POST, "subject");
        try{
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");
        
            try{
                    $stmt = $dbHandler->prepare("INSERT INTO  `lessons` (`name`, `subject`, `groupId`) VALUES (:lessonName, :subjects, :groupID);");
                    $stmt->bindParam("lessonName", $lessonName, PDO::PARAM_STR);
                    $stmt->bindParam("subjects", $subject, PDO::PARAM_STR);
                    $stmt->bindParam("groupID", $groupID, PDO::PARAM_STR);
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
                    <li><a id="NavText" href="dashboard.php">Dashboard</a></li>
                </ul>
            </nav>
        </header>
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
                        <option value='groupID' id='groupID'>
                            <?php
                            try {
                                $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");
                                $dbHandler->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
                            
                                $stmt = $dbHandler->prepare("SELECT `name` FROM `groups` WHERE `username` = :username");
                                $stmt->bindParam(":username", $username, PDO::PARAM_STR);
                                $stmt->execute();
                                $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
                            
                                foreach ($results as $result) {
                                    echo $result["name"];
                                }
                            } catch (PDOException $ex) {
                                $errors[] = $ex->getMessage();
                            }
                            ?>

                            </option>
                        </select>
                    </div>
                </div>
                <div class="loginprompt">
                    <div>
                        <label for="subject">Kies een vak:</label>
                        <select name="subject" id="subject">
                            <?php
                                $stmt = $dbHandler->prepare("SELECT subjects FROM `groups` WHERE `username` = :username");
                                $stmt->bindParam("username", $username, PDO::PARAM_STR);
                                $stmt->execute();
                                $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
                                
                                foreach($results as $result){
                                    foreach($result as $subjectresult){
                                        $endresults = explode(', ', $subjectresult);
                                        foreach($endresults as $endresult){
                                            echo "<option value='subject' id='subject'>$endresult</option>";
                                        }
                                    }
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
