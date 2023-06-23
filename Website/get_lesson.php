<?php
  require_once 'constants.php'; //Add the page for the database login constants 

  try
  {
    $dbHandler = new PDO("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", $dbuser, $dbpassword); //Makes a connection with the database

    try
    {
      // Check if the user has put in a lesson id
      if (isset($_GET['code']))
      {
        $lessonId = $_GET['code']; // Get the lesson id from the url
        $stmt = $dbHandler->prepare("SELECT * FROM `lessons` WHERE `id` = :id"); // Prepare a statement to get all the lessons from the database
        $stmt->bindParam(":id", $lessonId, PDO::PARAM_INT); // Bind the lesson id to the statement
        $stmt->execute(); // Execute the statement
        $results = $stmt->fetchAll(PDO::FETCH_ASSOC); //Get everything out of the table
        echo json_encode($results[0]); // Show the first result as a json object

        // Get the models from the database
        $models = $results[0]['modelIDs'];
        $modelIDs = explode(",", $models);
        foreach ($modelIDs as $modelID)
        {
          $stmt = $dbHandler->prepare("SELECT * FROM `models` WHERE `id` = :id"); // Prepare a statement to get all the lessons from the database
          $stmt->bindParam(":id", $modelID, PDO::PARAM_INT); // Bind the lesson id to the statement
          $stmt->execute(); // Execute the statement
          $results = $stmt->fetchAll(PDO::FETCH_ASSOC); //Get everything out of the table
          echo json_encode($results[0]); // Show the first result as a json object
        }
      }
    }
    catch (Exception $ex)
    {
      die($ex); //if there is something wrong with the connection or the insertion into the database, abort the mission. 
    }
  }
  catch (Exception $ex)
  {
    die($ex);
  }
?>