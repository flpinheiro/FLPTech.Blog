## 🧩O que é fixture
Fixture é tudo que prepara o estado inicial necessário para um teste rodar.
Ela cria o contexto: objetos, dados, conexões, arquivos, banco em memória etc.
É usada quando você precisa configurar o ambiente antes de testar algo.

### Use quando:

* Você precisa montar dados ou objetos repetidos em vários testes.

* Quer evitar código duplicado no setup.

* Precisa garantir que o teste começa sempre no mesmo estado.

## 🧪 O que é stub
Stub é um test double que retorna respostas pré-programadas.
Ele não verifica comportamento; apenas fornece dados quando chamado.

### Use quando:

* Você quer isolar o teste de dependências externas (API, banco, filesystem).

* Só precisa de um retorno previsível para continuar o fluxo do teste.

* Não importa como o método foi chamado, apenas que o teste avance.

### Exemplo mental:  
Um serviço de e-mail que sempre “finge” enviar e retorna true.

## 🎭 O que é mock
Mock é um test double que, além de simular comportamento, é configurado com expectativas de chamadas: quantas vezes, com quais argumentos, em qual ordem.

### Use quando:

* Você quer testar interações (estilo London):
“O método X deve chamar Y com o parâmetro Z.”

* O comportamento esperado depende da comunicação entre objetos.

* Você precisa garantir que uma dependência foi usada corretamente.

### Exemplo mental:  
Verificar que EmailService.send() foi chamado exatamente uma vez.