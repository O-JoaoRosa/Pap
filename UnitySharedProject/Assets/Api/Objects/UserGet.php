<?php // Executa o get(id) do User na BD

if($_SERVER['REQUEST_METHOD'] == "GET") {
    // Abre a lisgação
    require_once('../dbConnect.php');

    // Ativa/destaiva as mensagens de debug para postman
    $debug_On = false;

    // controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
    $recordsFound = -1;

    // Obtém oo id do url enviado pela app Android
    $userInfo = $_GET['UserInfo'];
    $Password = $_GET['Password'];

    // Prepara e executa a query e recebe o resultado num objeto dataset
    $sql = "SELECT * FROM user WHERE UserName = '$userInfo' OR Email = '$userInfo' AND Password = sha2('$Password',512);";

    //resposta do dbms
    $dbmsResponse = mysqli_query($con, $sql) or die("Erro #002: query failed");

    if (mysqli_num_rows($dbmsResponse) > 0){
        echo "5: erro more than 1 user with the same info found";

        // fecha a ligação
        mysqli_close($con);

        exit();
    }

    $row = mysqli_fetch_assoc($dbmsResponse);
    echo "0\t" + $row["UserName"] +"\t" + $row["Email"] + "\t" + $row["Money"] + "\t" + $row["Reputation"] + "\t" +  $row["UserCarIDSelected"];

    // fecha a ligação
    mysqli_close($con);
    exit();

    //obtém número de linhas da tabela
    $recordsFound = intval(mysqli_affected_rows($con));

    // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
    if ($dbmsResponse) {
        if($debug_On) echo "\n DEBUG: GET: Há dados. vou extrair a contagem";

        // Se >0 registos => flag passa a false. Não pode eliminar.
        if ($recordsFound > 0) {
            if($debug_On) echo "\n DEBUG: GET: Registo obtido com sucesso.";

            // Row recebe o registo da base de dados
            $row = mysqli_fetch_assoc($dbmsResponse);

            // Array result para receber o registo
            $result = array();

            echo "0\t" + $row["UserName"] +"\t" . $row["Email"] + "\t" + $row["Money"] + "\t" + $row["Reputation"] + "\t" +  $row["UserCarIDSelected"];

        } else {
            echo "Nenhum registo foi encontrado.";
        }
    }

    // fecha a ligação
    mysqli_close($con);
}
?>