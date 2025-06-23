## MECANISMOS DE SEGURANÇA DA INFORMAÇÃO

## Política de Segurança da Informação PSI

O objetivo principal da política de segurança da informação (PSI) é garantir a proteção e segurança das informações da nossa Cooperativa, assegurando a confidencialidade, integridade e disponibilidade dos dados, além de cumprir regulamentações e normas em vigor. Para atingir esses objetivos, nossa PSI estabeleceu:

● DIRETRIZES

● PROCESSOS DE SEGURANÇA DA INFORMAÇÃO

● SEGURANÇA CORPORATIVA

● PAPÉIS E RESPONSABILIDADES

● DECLARAÇÃO DE RESPONSABILIDADE

● SANÇÕES DISCIPLINARES

[Veja nossa Política de Segurança da Informação](https://github.com/ICEI-PUC-Minas-PMV-SI/pmv-si-2025-1-pe5-t2-g2-cooperativa-coopgo/blob/main/docs/COOPGO%20-%20POL%C3%8DTICA%20DE%20SEGURAN%C3%87A%20DA%20INFORMA%C3%87%C3%83O%20%20(1).pdf)


## Cartilha

O principal objetivo da nossa cartilha de segurança da informação é disseminar conhecimentos e boas práticas para proteger as informações e os sistemas de informação contra ameaças e riscos, aumentando a conscientização e o nível de segurança dos usuários e da cooperativa.

[Veja nossa Cartilha de Segurança da Informação](https://github.com/ICEI-PUC-Minas-PMV-SI/pmv-si-2025-1-pe5-t2-g2-cooperativa-coopgo/blob/main/docs/Cartilha.pdf)


 
## Analise de vulnerabilidade acerca da aplicação da cooperativa
 
Nosso objetivo é mapear algumas vulnerabilidade que podem vir ocorrer na aplicação.

Algumas possibilidades de vulnerabilidades na aplicação back-end:


**A07:2021 – Falhas de Identificação e Autenticação.** 

Conhecida como Autenticação Quebrada. Essa vulnerabilidade está relacionadas a falhas de identificação
Nossa aplicação atualmente utiliza armazenamentos de dados de senhas, de texto simples ou com hash fraco;
Possui autenticação multifator ausente ou ineficaz;


**A09:2021 – Falhas de Monitoramento e Registro de Segurança.** 

Esta categoria visa auxiliar na detecção, escalonamento e resposta a violações ativas
Nossa aplicação não possui registro e monitoramento, como consequência as violações não podem ser detectadas.


**A02:2021 – Falhas criptográficas.**

Esta categoria trata da proteção inadequada de dados sensíveis em repouso ou em trânsito, como senhas, tokens, dados bancários e informações pessoais.
 
Nossa aplicação não utiliza criptografia para proteger os dados sensíveis armazenados no banco de dados e nem adota HTTPS nas comunicações. Como consequência, esses dados podem ser interceptados ou acessados por terceiros mal-intencionados, comprometendo a confidencialidade e a integridade das informações.

## DEPLOY DA APLICAÇÃO 


![pkt](https://github.com/ICEI-PUC-Minas-PMV-SI/pmv-si-2025-1-pe5-t2-g2-cooperativa-coopgo/blob/main/docs/imagem.png)
 









