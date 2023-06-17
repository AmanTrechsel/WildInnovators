<?php
  require_once 'constants.php';

  $upload_json = filter_input(INPUT_POST, "json");

  try
  {
    $dbHandler = new PDO("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", $dbuser, $dbpassword);

    try
    {
      $stmt = $dbHandler->prepare("INSERT INTO models (`jsonfile`) VALUES (:json)");
      $stmt->bindParam(":json", $upload_json, PDO::PARAM_STR);
      $stmt->execute();
    }
    catch (Exception $ex)
    {
      die($ex);
    }
  }
  catch (Exception $ex)
  {
    die($ex);
  }
?>