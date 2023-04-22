# Sample-Project

Esse projeto tem como finalidade solucionar um teste proposto de orquestração de chamadas (api) externas em uma única api de negócio.

Esse projeto possui o conceito de feature toogle para ligar ou desligar os serviços externos individualmente 
alterações de rotas das chamadas externas

Esse projeto tem as rotas externas definidas via appSettings para ser configurado no Devops no intuito de alterar 
a configuração por ambiente.

Utilizei a estrutura de Dal, Adapter mas poderia ser utilizado o repository direto no projeto ao invés da camada Dal e feito 
os adapters dentro da model.

Regras de calculos achei mais interessante deixar dentro da model investimento focando mais no conceito de negócio do objeto 

Um ponto que poderia ser melhorado dependendo do cenário: utilizar pattern strategy de desenvolvimento
        
Uma dúvida que ficou em aberto é a questão de volumetria e na necessidade de ser assicrono, para fins do exercicio deixei sem task


##todo

docker build -t extrato-image .
docker-compose -f docker-compose.yml up
