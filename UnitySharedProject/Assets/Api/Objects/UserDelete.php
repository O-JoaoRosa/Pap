<?php // Executa o Delete do User na BD

    if ($_SERVER['REQUEST_METHOD']=='GET') {    // só aceita comunicações com método GET.

        //Executa a ligação
        require_once('../dbConnect.php');

        // Ativa/destaiva as mensagens de debug para postman
        $debug_On = false;

        // Obtém o id a partir do keyValuepair do url, enviado pela app Mobile
        $id = $_GET['id'];

        ///////////////////////////////////////////////////////////////////////////////////////////
        /// VIR: Verificação de violação da integridade relacional
        /// Antes de eliminar o registo, verifica se as tabelas com fks para a tabela, cujo id
        /// está a ser eliminado (ver quais no VP) têm registos ativos para o ID em Delete.
        /// Se sim há notifica e aborta. Se não, elimina.
        //////////////////////////////////////////////////////////////////////////////////////////

        // Flag de controlo de violação de interidade
        $deleteOk = true;   // ativa-se quando não há fks em tabelas relacionadas

        // controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
        $recordsFound = -1;

        if ($debug_On) echo "DEBUG: ID: " . $id . " Vou verificar vir!";

        ////////////////////////////////////////////////////////////////////////////////////
        // O bloco seguinte é para repetir tantas vezes como as tabelas com FK para o User
        // Verifica na tabela indicada se há VIR para o id indicado

        // query: A contagem é devolvida num atributo com nome qty, com 0 ou N contados
        $sql = "select COUNT(*) as qty FROM UserTerminal WHERE UserID=$id;";

        //echo "\n VIR: Query a executar: ".$sql;
        $dbmsResponse = mysqli_query($con, $sql);

        if ($debug_On) echo "\n DEBUG: VIR: Executei a query. Vou verificar se há dados devolvidos";

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: VIR: Há dados. vou extrair a contagem";
            $recordsFound = mysqli_fetch_assoc($dbmsResponse)['qty'];

            if ($debug_On) echo "\n DEBUG: VIR: count: " . $recordsFound;

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: VIR: Flag carimbada com false:";

                // bloqueia 0 delete
                $deleteOk = false;
            }

            // preparar para as próximas tabelas
            mysqli_free_result($dbmsResponse);
            $recordsFound = -1;
        } else {
            echo "\n DBMS indisponível: " . $dbmsResponse;
        }

        // fim do bloco de Controlo da fk desta tabela
        // Cada tabela que tenha fks para a tabela, cujo id está a ser eliminado,
        // tem que ter um bloco identico, a colocar a seguir
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        // O bloco seguinte é para repetir tantas vezes como as tabelas com FK para o User
        // Verifica na tabela indicada se há VIR para o id indicado

        // query: A contagem é devolvida num atributo com nome qty, com 0 ou N contados
        $sql = "select COUNT(*) as qty FROM UserConfig WHERE UserID=$id;";

        //echo "\n VIR: Query a executar: ".$sql;
        $dbmsResponse = mysqli_query($con, $sql);

        if ($debug_On) echo "\n DEBUG: VIR: Executei a query. Vou verificar se há dados devolvidos";

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: VIR: Há dados. vou extrair a contagem";
            $recordsFound = mysqli_fetch_assoc($dbmsResponse)['qty'];

            if ($debug_On) echo "\n DEBUG: VIR: count: " . $recordsFound;

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: VIR: Flag carimbada com false:";

                // bloqueia 0 delete
                $deleteOk = false;
            }

            // preparar para as próximas tabelas
            mysqli_free_result($dbmsResponse);
            $recordsFound = -1;
        } else {
            echo "\n DBMS indisponível: " . $dbmsResponse;
        }

        // fim do bloco de Controlo da fk desta tabela
        // Cada tabela que tenha fks para a tabela, cujo id está a ser eliminado,
        // tem que ter um bloco identico, a colocar a seguir
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        // O bloco seguinte é para repetir tantas vezes como as tabelas com FK para o User
        // Verifica na tabela indicada se há VIR para o id indicado

        // query: A contagem é devolvida num atributo com nome qty, com 0 ou N contados
        $sql = "select COUNT(*) as qty FROM Notification WHERE UserID=$id;";

        //echo "\n VIR: Query a executar: ".$sql;
        $dbmsResponse = mysqli_query($con, $sql);

        if ($debug_On) echo "\n DEBUG: VIR: Executei a query. Vou verificar se há dados devolvidos";

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: VIR: Há dados. vou extrair a contagem";
            $recordsFound = mysqli_fetch_assoc($dbmsResponse)['qty'];

            if ($debug_On) echo "\n DEBUG: VIR: count: " . $recordsFound;

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: VIR: Flag carimbada com false:";

                // bloqueia 0 delete
                $deleteOk = false;
            }

            // preparar para as próximas tabelas
            mysqli_free_result($dbmsResponse);
            $recordsFound = -1;
        } else {
            echo "\n DBMS indisponível: " . $dbmsResponse;
        }

        // fim do bloco de Controlo da fk desta tabela
        // Cada tabela que tenha fks para a tabela, cujo id está a ser eliminado,
        // tem que ter um bloco identico, a colocar a seguir
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        // O bloco seguinte é para repetir tantas vezes como as tabelas com FK para o User
        // Verifica na tabela indicada se há VIR para o id indicado

        // query: A contagem é devolvida num atributo com nome qty, com 0 ou N contados
        $sql = "select COUNT(*) as qty FROM Profile WHERE UserID=$id;";

        //echo "\n VIR: Query a executar: ".$sql;
        $dbmsResponse = mysqli_query($con, $sql);

        if ($debug_On) echo "\n DEBUG: VIR: Executei a query. Vou verificar se há dados devolvidos";

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: VIR: Há dados. vou extrair a contagem";
            $recordsFound = mysqli_fetch_assoc($dbmsResponse)['qty'];

            if ($debug_On) echo "\n DEBUG: VIR: count: " . $recordsFound;

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: VIR: Flag carimbada com false:";

                // bloqueia 0 delete
                $deleteOk = false;
            }

            // preparar para as próximas tabelas
            mysqli_free_result($dbmsResponse);
            $recordsFound = -1;
        } else {
            echo "\n DBMS indisponível: " . $dbmsResponse;
        }

        // fim do bloco de Controlo da fk desta tabela
        // Cada tabela que tenha fks para a tabela, cujo id está a ser eliminado,
        // tem que ter um bloco identico, a colocar a seguir
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        // O bloco seguinte é para repetir tantas vezes como as tabelas com FK para o User
        // Verifica na tabela indicada se há VIR para o id indicado

        // query: A contagem é devolvida num atributo com nome qty, com 0 ou N contados
        $sql = "select COUNT(*) as qty FROM OrganizationUser WHERE UserID=$id;";

        //echo "\n VIR: Query a executar: ".$sql;
        $dbmsResponse = mysqli_query($con, $sql);

        if ($debug_On) echo "\n DEBUG: VIR: Executei a query. Vou verificar se há dados devolvidos";

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: VIR: Há dados. vou extrair a contagem";
            $recordsFound = mysqli_fetch_assoc($dbmsResponse)['qty'];

            if ($debug_On) echo "\n DEBUG: VIR: count: " . $recordsFound;

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: VIR: Flag carimbada com false:";

                // bloqueia 0 delete
                $deleteOk = false;
            }

            // preparar para as próximas tabelas
            mysqli_free_result($dbmsResponse);
            $recordsFound = -1;
        } else {
            echo "\n DBMS indisponível: " . $dbmsResponse;
        }

        // fim do bloco de Controlo da fk desta tabela
        // Cada tabela que tenha fks para a tabela, cujo id está a ser eliminado,
        // tem que ter um bloco identico, a colocar a seguir
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        /// DELETE - caso a flag deleteOK esteja true
        ////////////////////////////////////////////////////////////////////////////////////

        if ($debug_On) echo "\n DEBUG: Saí do VIR: vou testar a flag delete";

        // Se a flag é true, permite delete. Caso contrário notifica o user.
        if ($deleteOk) {
            if ($debug_On) echo "\n DEBUG: Há delete => vou eliminar: ";

            //Query a executar com os dados recebidos do Cliente
            $sql = "DELETE FROM `User` WHERE ID=$id;";

            // Executa a query no DBMS e devolve o resultado ao Cliente
            if (mysqli_query($con, $sql)) {

                if ($debug_On) echo "\nDEBUG: Registos executados: " . mysqli_affected_rows($con) . " -> ";

                // Verifica o resultado do delete.
                if (mysqli_affected_rows($con) > 0) {
                    echo "DBMS OK: \nRegisto Eliminado com Sucesso!";
                } else {
                    echo "DBMS ERRO: \nRegisto não existe!";
                }

            } else {
                echo "DBMS ERRO: " . mysqli_error($con);
            }
        } else {
            echo "DBMS VIR: Há Registos relacionados em Matrículas";
        }

        if ($debug_On) echo "\n DEBUG: vou fechar a ligação";

        // Fecha ligação
        mysqli_close($con);
    }
?>"