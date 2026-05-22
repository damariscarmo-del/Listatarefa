    const myForm1 = document.getElementById('Tarefa');
if (myForm1 != null) {
myForm1.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('https://localhost:7111/Tarefa/cadastrar', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            status: document.getElementById("status").value,
            descricao: document.getElementById("descricao").value
        }),
    }).then(response => {
        if (response.status ==401){
            alert ("Faça login antes de cadastrar!");
            window.location.href="index.html";
        }
        response.json();})
        .then(data => {
            document.getElementById("respostaTarefa").innerHTML ="<h4>Tarefa cadastrada com sucesso!</h4>";        
        })
});
}

fetch('https://localhost:7111/Tarefa/TarefaUsuario/',
    { 
        credentials: 'include' 
})
    .then(response => {

            return response.json();
        }
       
    )
    .then(data => {
        console.log (data);
        if(data.length >0){
        var resposta = document.getElementById("respostaConsulta");
        resposta.innerHTML = "<h4>Segue Lista de suas Tarefas</h4> ";
        for (i = 0; i < data.length; i++) {
            resposta.innerHTML += "<li> Cliente: " + data[i].cliente + "</li>";
            resposta.innerHTML += "status (status): <input type='text' id='status"+data[i].Tarefa+"' value='" + data[i].status + "'>";
            resposta.innerHTML += "descricao: <input type='number' id='descricao"+data[i].Tarefa+"' value='" + data[i].descricao + "'>";
            resposta.innerHTML += "<button onclick='editaTarefa("+data[i].reservas+")'>Editar Tarefa </button>";
            resposta.innerHTML += "<button onclick='deletaTarefa("+data[i].reservas+")'>Deletar Tarefa </button> <hr>";

        }
    }
    });

    function deletaTarefa(idTarefa){
        fetch('https://localhost:7111/Tarefa/'+idTarefa, {
            method: 'DELETE', 
            credentials: 'include'
  
        }).then(response => {
            alert("Tarefa excluída");
            window.location.href="index.html";
        })
    }

    function editaTarefa (idTarefa){
        fetch('https://localhost:7111/Tarefa/'+idTarefa, {
            method: 'PUT',   
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                status: (document.getElementById("status"+idTarefa).value),
                descricao: document.getElementById("descricao"+idTarefa).value
            }),
        }).then(response => {
            if (response.status ==401){
                alert ("Faça login antes de editar!");
                window.location.href="index.html";
            }else{
                alert ("Tarefa editada!");
            }})
           
    }