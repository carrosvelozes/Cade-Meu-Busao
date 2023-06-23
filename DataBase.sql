#######################################################
#                PROJETO INTERDISCIPLINAR             #
#######################################################
#          BANCO DE DADOS CONSTRUIDO EM 3FN           #
#           DE ACORDO COM OS SLIDES DA AULA           #
#######################################################
#               LEONARDO MORAES CP3015777             #
#               LUCAS CARVALHO CP3020363              #
#######################################################


DROP SCHEMA IF EXISTS cadastro;
CREATE SCHEMA IF NOT EXISTS cadastro;
USE cadastro;

#######################################################
#######################TABELAS#########################
#######################################################

CREATE TABLE passageiros (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(50) DEFAULT NULL,
  nascimento DATE DEFAULT NULL,
  email VARCHAR(50) DEFAULT NULL,
  senha VARCHAR(32) DEFAULT NULL
);

ALTER TABLE passageiros ADD INDEX idx_nome (nome);

CREATE TABLE linhas_onibus (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(300) DEFAULT NULL,
  numero VARCHAR(50) DEFAULT NULL
);

ALTER TABLE linhas_onibus ADD INDEX idx_numero (numero);

CREATE TABLE paradas (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT DEFAULT NULL,
  nome VARCHAR(20) DEFAULT NULL,
  endereco VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id)
);

CREATE TABLE linhas_paradas (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT NOT NULL,
  parada_id INT NOT NULL,
  ordem VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id),
  FOREIGN KEY (parada_id) REFERENCES paradas(id)
);

CREATE TABLE dias_semana (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  descricao VARCHAR(20) DEFAULT NULL
);

CREATE TABLE horarios (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT NOT NULL,
  dia_semana_id INT NOT NULL,
  horario_saida VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id),
  FOREIGN KEY (dia_semana_id) REFERENCES dias_semana(id)
);

CREATE TABLE veiculos (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT DEFAULT NULL,
  placa VARCHAR(50) DEFAULT NULL,
  modelo VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id)
);

CREATE TABLE nomes_motoristas (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(50) DEFAULT NULL
);

CREATE TABLE motoristas (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT DEFAULT NULL,
  nome_id INT DEFAULT NULL,
  cnh VARCHAR(11) DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id),
  FOREIGN KEY (nome_id) REFERENCES nomes_motoristas(id)
);

CREATE TABLE problemas_tipos (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  descricao VARCHAR(300) DEFAULT NULL
);

CREATE TABLE problemas_reportados (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  descricao VARCHAR(300) DEFAULT NULL,
  data_hora DATE DEFAULT NULL,
  tipo_problema_id INT DEFAULT NULL,
  usuario_id INT DEFAULT NULL,
  linha_id INT DEFAULT NULL,
  FOREIGN KEY (linha_id) REFERENCES linhas_onibus(id),
  FOREIGN KEY (usuario_id) REFERENCES passageiros(id),
  FOREIGN KEY (tipo_problema_id) REFERENCES problemas_tipos(id)
);


#######################################################
#######################INSERTS#########################
#######################################################

INSERT INTO passageiros (nome, email, senha, nascimento) VALUES
('Leonardo', 'leonardo@exemplo.com', '123', '2023-01-01'),
('Lucas', 'lucas@exemplo.com', '123', '2023-01-02'),
('Joao', 'joao@exemplo.com', '123', '2023-01-01'),
('Pedro', 'pedro@exemplo.com', '123', '2023-01-01'),
('Carla', 'carla@exemplo.com', '123', '2023-01-01'),
('Joana', 'joana@exemplo.com', '123', '2023-01-01'),
('Gabriela', 'gabriela@exemplo.com', '123', '2023-01-01'),
('Matias', 'matias@exemplo.com', '123', '2023-01-01'),
('Jefersson', 'jefersson@exemplo.com', '123', '2023-01-01'),
('Jennifer', 'jennifer@exemplo.com', '123', '2023-01-02');

INSERT INTO linhas_onibus (nome, numero) VALUES
('Res. Sirius II', '225'),
('Res. Sirius', '226'),
('Res. Sirius', '224'),
('Terminal Itajai', '213'),
('Terminal Itajai', '212'),
('Jardim Florence II', '229'),
('Terminal Campo Grande', '211'),
('Princesa D*Oeste', '228'),
('Satelite Iris I', '231'),
('Satelite Iris III', '223');

INSERT INTO paradas (nome, endereco) VALUES
('Bairro', 'Res. Sirius II'),
('Bairro', 'Res. Sirius'),
('Bairro', 'Res. Sirius'),
('Bairro', 'Terminal Itajai'),
('Bairro', 'Terminal Itajai'),
('Bairro', 'Jardim Florence II'),
('Bairro', 'Terminal Campo Grande'),
('Bairro', 'Princesa D*Oeste'),
('Bairro', 'Satelite Iris I'),
('Bairro', 'Satelite Iris III');

INSERT INTO linhas_paradas (linha_id, parada_id, ordem) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(6, 6, 1),
(7, 7, 1),
(8, 8, 1),
(9, 9, 1),
(10, 10, 1);

INSERT INTO dias_semana (descricao) VALUES
('Segunda-feira'),
('Terça-feira'),
('Quarta-feira'),
('Quinta-feira'),
('Sexta-feira'),
('Sábado'),
('Domingo');

INSERT INTO horarios (linha_id, dia_semana_id, horario_saida) VALUES
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '08:00:00'),
(1, 1, '09:00:00');

INSERT INTO veiculos (placa, modelo, linha_id) VALUES
('JTR-3726', 'Mercedes-Benz', 1),
('XKP-4129', 'Mercedes-Benz', 2),
('LZX-9240', 'Mercedes-Benz', 3),
('QCY-6807', 'Mercedes-Benz', 4),
('HDF-1356', 'Mercedes-Benz', 5),
('NBT-7914', 'Mercedes-Benz', 6),
('WMS-2685', 'Mercedes-Benz', 7),
('PRV-5738', 'Mercedes-Benz', 8),
('VFG-9431', 'Mercedes-Benz', 9),
('YEH-6192', 'Mercedes-Benz', 10);

INSERT INTO nomes_motoristas (nome) VALUES
('Carlos Silva'),
('Luan Samurai'),
('Adilson Bigode'),
('Brenda Bom'),
('Dani Velocidade'),
('Enzo Mineiro'),
('Matheus Doismetros'),
('Gabriel Silva'),
('Carlos Silva'),
('Roberto Alves');

INSERT INTO motoristas (linha_id, nome_id, cnh) VALUES
(1, 1, '96845037258'),
(2, 2, '69048209105'),
(3, 3, '68539014712'),
(4, 4, '97832064534'),
(5, 5, '56210478532'),
(6, 6, '18957290345'),
(7, 7, '32795641803'),
(8, 8, '21983645075'),
(9, 9, '50381276492'),
(10, 10, '87014936258');

INSERT INTO problemas_tipos (descricao) VALUES
('Problema de atraso'),
('Problema de lotacao'),
('Problema de seguranca'),
('Qualidade do servico'),
('Outros');

INSERT INTO problemas_reportados (linha_id, tipo_problema_id, usuario_id, descricao, data_hora) VALUES
(1, 1, 1, 'Teste', '2023-06-01'),
(2, 1, 2, 'Teste', '2023-06-01'),
(3, 1, 3, 'Teste', '2023-06-01'),
(4, 1, 4, 'Teste', '2023-06-01'),
(5, 1, 5, 'Teste', '2023-06-01'),
(6, 1, 6, 'Teste', '2023-06-01'),
(7, 1, 7, 'Teste', '2023-06-01'),
(8, 1, 8, 'Teste', '2023-06-01'),
(9, 1, 9, 'Teste', '2023-06-01'),
(10, 1, 10, 'Teste', '2023-06-01');


#######################################################
#######################SELECTS#########################
#######################################################

#selecao dos dados da linha 225
SELECT * FROM linhas_onibus WHERE numero = 225;

#selecao do total de feedbacks utilizando o metodo COUNT e tambem qual o tipo de problema mais reportado sobre a linha 225
SELECT COUNT(*) AS TotalProblemas, problemas_tipos.descricao AS ProblemasMaisReportados
FROM problemas_reportados
INNER JOIN linhas_onibus ON problemas_reportados.linha_id = linhas_onibus.id
INNER JOIN problemas_tipos ON problemas_reportados.tipo_problema_id = problemas_tipos.id
WHERE linhas_onibus.numero = 225
GROUP BY problemas_tipos.descricao
ORDER BY COUNT(*) DESC 
LIMIT 1;


#selecao do feedback enviado pelo passageiro sobre a linha 225
SELECT problemas_reportados.descricao AS Feedback, problemas_reportados.data_hora AS Data, problemas_tipos.descricao AS Tipo
FROM linhas_onibus
INNER JOIN problemas_reportados ON linhas_onibus.id = problemas_reportados.linha_id
INNER JOIN problemas_tipos ON problemas_reportados.tipo_problema_id = problemas_tipos.id
WHERE linhas_onibus.numero = 225;

#selecao das informacoes da placa do veiculo da linha 255
SELECT veiculos.placa
FROM veiculos
JOIN linhas_onibus ON veiculos.linha_id = linhas_onibus.id
WHERE linhas_onibus.numero = 225;


#selecao do nome do motorista vinculado a linha 225
SELECT nomes_motoristas.nome
FROM motoristas
JOIN linhas_onibus ON motoristas.linha_id = linhas_onibus.id
JOIN nomes_motoristas ON motoristas.nome_id = nomes_motoristas.id
WHERE linhas_onibus.numero = 225;

#consulta a tabela problemas_reportados e obtem a contagem de problemas para cada problema em problemas_tipos
SELECT tipo_problema_id, COUNT(*) AS total FROM problemas_reportados WHERE tipo_problema_id IN (1, 2, 3, 4, 5) GROUP BY tipo_problema_id;

#consulta a tabela para a linha com maior numero de reclamacoes
SELECT linhas_onibus.numero
FROM linhas_onibus 
INNER JOIN problemas_reportados 
ON linhas_onibus.id = problemas_reportados.linha_id 
GROUP BY linhas_onibus.numero 
ORDER BY COUNT(problemas_reportados.descricao) DESC LIMIT 1;

#consulta o numero total de feedbacks
SELECT COUNT(id) AS quantidade_reclamacoes FROM problemas_reportados;

SELECT id FROM problemas_tipos WHERE descricao = @selectedProblemType;

SELECT paradas.nome
FROM paradas
JOIN linhas_onibus
ON linhas_onibus.id = linhas_onibus.id
WHERE linhas_onibus.numero = 225
                
               
                
                