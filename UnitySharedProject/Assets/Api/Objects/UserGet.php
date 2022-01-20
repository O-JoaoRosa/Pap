<?php // Executa o get(id) do User na BD

if($_SERVER['REQUEST_METHOD'] == "GET") {
    // Abre a lisgação
    require_once('../dbConnect.php');

    // Ativa/destaiva as mensagens de debug para postman
    $debug_On = false;

    // controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
    $recordsFound = -1;

    // Obtém oo id do url enviado pela app Android
    $id = $_GET['id'];

    if($debug_On) echo "DEBUG: Dados \n ID='$id";

    // Prepara e executa a query e recebe o resultado num objeto dataset
    $sql = "SELECT * FROM `User` WHERE ID = $id;";

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

            // Grava o registo no array result
            array_push($result,array(
                "ID"=>$row['ID'],							// Atributo ID, seguido do seu valor
                "UserName"=>$row['UserName'],						// Atributo Name, seguido do seu valor
                "Image"=>$row['Image'],			    // Atributo Nickname, seguido do seu valor
                "Email"=>$row['Email'],			            // Atributo Email, seguido do seu valor
                "Money"=>$row['Money'],	                // Atributo Gender, seguido do seu valor
                "Reputation"=>$row['Reputation'],			    // Atributo BornDate, seguido do seu valor
                "Password"=>$row['Password'],			    // Atributo Password, seguido do seu valor
                "LastTimeOnline"=>$row['UsLastTimeOnlineerPicURL'],
                "UserCarIDSelected"=>$row['UserCarIDSelected'],	        // Atributo UserPicURL, seguido do seu valor
            ));

            echo json_encode(array('result' => $result));
        } else {
            echo "Nenhum registo foi encontrado.";
        }
    }

    // fecha a ligação
    mysqli_close($con);
}
?>