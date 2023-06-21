DROP SCHEMA IF exists cadastro;
CREATE SCHEMA IF NOT EXISTS cadastro;
USE cadastro;

#######################################################
#######################TABELAS#########################
#######################################################

CREATE TABLE passageiros (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome varchar(50) DEFAULT NULL,
  nascimento varchar(50) DEFAULT NULL,
  email varchar(50) DEFAULT NULL,
  senha varchar(32) DEFAULT NULL
);

CREATE TABLE linhas_onibus (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome varchar(20) DEFAULT NULL,
  numero varchar(50) DEFAULT NULL,
  descricao varchar(50) DEFAULT NULL
);

CREATE TABLE paradas (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome varchar(20) DEFAULT NULL,
  endereco varchar(50) DEFAULT NULL
);

CREATE TABLE linhas_paradas (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id int NOT NULL,
  parada_id int NOT NULL,
  ordem varchar(50) DEFAULT NULL,
  foreign key (linha_id) references linhas_onibus(id),
  foreign key (parada_id) references paradas(id)
);

CREATE TABLE horarios (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT NOT NULL,
  horario_saida varchar(50) DEFAULT NULL,
  dia_semana varchar(50) DEFAULT NULL,
  foreign key (linha_id) references linhas_onibus(id)
);

CREATE TABLE veiculos (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  linha_id INT NOT NULL,
  placa varchar(50) DEFAULT NULL,
  modelo varchar(50) DEFAULT NULL,
  foreign key (linha_id) references linhas_onibus(id)
);

CREATE TABLE motoristas (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome varchar(50) DEFAULT NULL,
  cnh varchar(11) DEFAULT NULL
);

CREATE TABLE problemas_tipos (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  descricao varchar(300) DEFAULT NULL
);

CREATE TABLE problemas_reportados (
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  descricao varchar(300) DEFAULT NULL,
  data_hora date DEFAULT NULL,
  tipo_problema_id INT(50) DEFAULT NULL,
  usuario_id INT(255) DEFAULT NULL,
  linha_id INT(255) DEFAULT NULL,
  foreign key (linha_id) references linhas_onibus(id),
  foreign key (usuario_id) references passageiros(id),
  foreign key (tipo_problema_id) references problemas_tipos(id)
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

INSERT INTO linhas_onibus (nome, numero, descricao) VALUES
('Linha Verde', '200', 'Linha que percorre a zona norte até a zona sul'),
('Linha Vermelho', '210', 'Linha que percorre a zona norte até a zona sul'),
('Linha Azul', '230', 'Linha que percorre a zona norte até a zona sul'),
('Linha Amarela', '240', 'Linha que percorre a zona norte até a zona sul'),
('Linha Rosa', '250', 'Linha que percorre a zona norte até a zona sul'),
('Linha Roxa', '260', 'Linha que percorre a zona norte até a zona sul'),
('Linha Marrom', '270', 'Linha que percorre a zona norte até a zona sul'),
('Linha Laranja', '280', 'Linha que percorre a zona norte até a zona sul'),
('Linha Cinza', '290', 'Linha que percorre a zona norte até a zona sul'),
('Linha Preta', '300', 'Linha que percorre a zona norte até a zona sul');



INSERT INTO paradas (nome, endereco) VALUES
('Parada Central','Praça da Sé, São Paulo'),
('Parada Leste','Rua da Consolação, São Paulo');

INSERT INTO linhas_paradas (linha_id, parada_id, ordem) VALUES
(1, 1, 1),
(1, 2, 2);

INSERT INTO horarios (linha_id, dia_semana, horario_saida) VALUES
(1, 'Segunda-feira', '08:00:00'),
(1, 'Segunda-feira', '09:00:00');

INSERT INTO veiculos (placa, modelo, linha_id) VALUES
('ABC-1234', 'Mercedes-Benz', 1),
('XYZ-5678', 'Volvo', 2);

INSERT INTO motoristas (nome, cnh) VALUES
('Carlos Silva', '12345678'),
('Roberto Alves', '87654321');

INSERT INTO problemas_tipos (descricao) VALUES
('Problema de atraso'),
('Problema de lotacao'),
('Problema de seguranca'),
('Qualidade do servico'),
('Outros');

INSERT INTO problemas_reportados (linha_id, tipo_problema_id, usuario_id, descricao, data_hora) VALUES
(1, 1, 1, 'O ônibus estava atrasado hoje', '2023-06-01'),
(2, 2, 2, 'O ônibus estava atrasado hoje', '2023-06-01'),
(3, 3, 3, 'O ônibus estava atrasado hoje', '2023-06-01'),
(4, 4, 4, 'O ônibus estava atrasado hoje', '2023-06-01'),
(5, 5, 5, 'O ônibus estava atrasado hoje', '2023-06-01'),
(6, 4, 6, 'O ônibus estava atrasado hoje', '2023-06-01'),
(7, 3, 7, 'O ônibus estava atrasado hoje', '2023-06-01'),
(8, 2, 8, 'O ônibus estava atrasado hoje', '2023-06-01'),
(9, 1, 9, 'O ônibus quebrou no meio da viagem','2023-06-01'),
(10, 2, 10, 'O ônibus estava atrasado hoje', '2023-06-01');