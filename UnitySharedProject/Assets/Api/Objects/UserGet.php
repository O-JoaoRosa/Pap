<?php // Executa o select(id) do Profile na BD

if($_SERVER['REQUEST_METHOD'] == "GET") {
    // Abre a lisgação
    require_once('../dbConnect.php');

    // Ativa/destaiva as mensagens de debug para postman
    $debug_On = false;

    // controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
    $recordsFound = -1;

    // Obtém oo id do url enviado pela app Android
    $username = $_GET['username'];
    $password = $_GET['password'];

    if($debug_On) echo "DEBUG: Dados \n Nickname='$username \n Password=********";

    // Prepara e executa a query e recebe o resultado num objeto dataset
    $sql = "SELECT `user`.* FROM `user` WHERE `UserName` = '$username' and `Password` = SHA2('$password', 512);";

    //resposta do dbms
    $dbmsResponse = mysqli_query($con, $sql);

    //obtém número de linhas da tabela
    $recordsFound = intval(mysqli_affected_rows($con));

    // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
    if ($dbmsResponse) {
        if($debug_On) echo "\n DEBUG: GET: Há dados. vou extrair a contagem";

        // Se >0 registos => flag passa a false. Não pode eliminar.
        if ($recordsFound > 0) {
            if($debug_On) echo "\n DEBUG: GET: Registo obtido com sucesso.";

            // Row recebe o registo da base de dados
            $row = mysqli_fetch_array($dbmsResponse);

            // Array result para receber o registo
            $result = array();

            //echo "SERVER: ID#".$row['ID']." - ".$row["UserName"]." - ".$row["Image"]." - ".$row["Money"]." - ".$row["Reputation"]." - ".$row["UserCarIDSelected"];

            // Grava o registo no array result
            array_push($result,array(
                "ID"=>$row['ID'],                    // Atributo UserID, seguido do seu valor
                "UserName"=>$row['UserName'],            // Atributo UserTypeID, seguido do seu valor
                "Image"=>$row['Image'],            // Atributo CreateDate, seguido do seu valor
                "Money"=>$row['Money'],            // Atributo LoginStatus, seguido do seu valor
                "Reputation"=>$row['Reputation'],            // Atributo LoginStatus, seguido do seu valor
                "UserCarIDSelected"=>$row['UserCarIDSelected'],            // Atributo LoginStatus, seguido do seu valor
            ));

            echo json_encode(array($result));
        } else {
            echo "\nNenhum registo foi encontrado.";
        }
    }

    // fecha a ligação
    mysqli_close($con);
}
?>
