## Introdução
Este documento tem como propósito apresentar, de forma clara e objetiva, a estrutura de rede da CoopGo. Nele são descritos os principais recursos utilizados, a organização do ambiente e as práticas adotadas para garantir desempenho e segurança. O conteúdo serve como base para decisões estratégicas relacionadas à manutenção, crescimento e avaliação do ambiente tecnológico.

## Contexto
A cooperativa bancária COOPGO, fundada na cidade de Uberaba em 2022, possui o objetivo de equilibrar a justiça social com a prosperidade econômica e a sustentabilidade com os resultados financeiros, respeitando os interesses coletivos e as aspirações individuais. Ao longo do tempo, a cooperativa foi crescendo e viu a necessidade de expansão geográfica. Com isso, foram criadas três filiais em cidades próximas da Matriz, nas cidades de Ituiutaba, Frutal e Monte Carmelo.
Para isso, é importante um projeto de infraestrutura de redes bem elaborado para que a matriz e suas filiais possuam melhor comunicação, com qualidade e segurança.

## Projeto de infraestrutura de Rede
O objetivo do projeto é desenvolver uma infraestrutura de redes robusta e escalável que atenda às necessidades operacionais da cooperativa bancária, garantindo alta disponibilidade por meio da implementação de redundância para evitar interrupções nos serviços. Além disso, busca-se um desempenho otimizado com o uso de balanceamento de carga e otimização de tráfego para reduzir a latência. Outra meta é assegurar a escalabilidade com uma estrutura modular que permita crescimento sem impactar o desempenho, juntamente com um gerenciamento centralizado pela implementação de monitoramento e ferramentas de gestão para visibilidade e controle da rede.

## Departamentos da COOPGO
| Setor                     | Matriz | Ituiutaba | Frutal | Monte Carmelo | 
|---------------------------|--------|-----------|--------|---------------|
| Gerência e Administração  | 3      | 1         | 1      | 1             | 
| Crédito e cobrança        | 3      | 2         | 2      | 1             | 
| TI e Infraestrutura       | 2      | 1         | 1      | 1             | 
| Atendimento ao coperado   | 4      | 3         | 3      | 2             | 
| Auditoria interna         | 2      | 1         | 1      | 1             | 
| Total                     | **14** | **8**     | **8**  | **6**         |

## Esboço do projeto de Infraestrutura de Rede – Cooperativa Bancária COOPGO
![pkt](https://github.com/ICEI-PUC-Minas-PMV-SI/pmv-si-2025-1-pe5-t2-g2-cooperativa-coopgo/blob/main/docs/Captura%20de%20tela%202025-06-10%20185648.png)

 

##  Visão Geral - Esboço do projeto no Cisco Packet Tracer 

O projeto de rede da COOPGO foi estruturado para atender às necessidades de conectividade, desempenho e segurança das operações entre a matriz (em Uberaba) e suas três filiais localizadas em Ituiutaba, Monte Carmelo e Frutal. A topologia contempla segmentação lógica da rede (VLANs), conexão ponto-a-ponto entre unidades, além da organização por departamentos e serviços críticos (servidores, autenticação, e infraestrutura de rede).

---

##  Disposição Física e Lógica da Rede

### 1 Matriz (Uberaba)

* **Rede local**: 192.168.0.0/24
* **Roteador de borda**: IP 192.168.0.1
* **Servidor DNS/Infraestrutura**: 192.168.0.2
* **Servidores de serviços internos**: IPs entre 192.168.0.10 a 192.168.0.24
* **Switch Core** centraliza a distribuição para os setores:

  * Gerência e Administração
  * Atendimento ao cooperado
  * Crédito e cobrança
  * Auditoria interna
  * TI e infraestrutura

Cada setor está representado por um bloco de máquinas identificadas como `COOPG0X`, com endereçamento estático.

### 2 Filial Ituiutaba

* **Rede local**: 192.168.1.0/24
* **Roteador de borda (GW)**: 192.168.1.1
* **Servidor DNS**: 192.168.1.2
* **Dispositivos de usuário final**: IPs de 192.168.1.10 a 192.168.1.18
* **Switch de distribuição** conecta servidores, setores administrativos e access points wireless.

### 3 Filial Monte Carmelo

* **Rede local**: 192.168.2.0/24
* **Roteador de borda**: 192.168.2.1
* **Servidor DNS/Infraestrutura**: 192.168.2.2
* **Clientes e setores internos** com IPs de 192.168.2.10 a 192.168.2.16
* Switch intermediário conectado ao backbone da matriz.

### 4 Filial Frutal

* **Rede local**: 192.168.3.0/24
* **Roteador de borda**: 192.168.3.1
* **Servidor DNS**: 192.168.3.2
* Dispositivos organizados por setores, com IPs entre 192.168.3.10 a 192.168.3.18

---

## 3. Conectividade Interfiliais

As conexões entre matriz e filiais são estabelecidas por meio de enlaces ponto-a-ponto seriais:

* **Serial 000**: Matriz (192.168.101.1) ↔ Filial Ituiutaba (192.168.101.2)
* **Serial 001**: Matriz (192.168.102.1) ↔ Filial Monte Carmelo (192.168.102.2)
* **Serial 010**: Matriz (192.168.103.1) ↔ Filial Frutal (192.168.103.2)

Essas conexões formam uma topologia estrela, com a matriz no centro, funcionando como o hub de comunicação para todas as filiais.

---

## 4. Segmentação Lógica (VLANs e DMZ)

A estrutura lógica da rede prevê a criação de VLANs por setor para segmentar e isolar o tráfego, garantindo segurança, performance e facilidade de gerenciamento:

* **VLAN 10** – Administração e Gerência
* **VLAN 20** – Crédito e Cobrança
* **VLAN 30** – Atendimento ao Cooperado
* **VLAN 40** – Auditoria Interna
* **VLAN 50** – TI e Infraestrutura
* **VLAN 60** – Servidores

Adicionalmente, uma **DMZ (Zona Desmilitarizada)** pode ser configurada na matriz para hospedar servidores públicos e expostos à internet (ex.: webmail, site institucional), separando-os da rede interna principal, com controle de acesso rigoroso via firewall.

---

## 5. Equipamentos Utilizados

* **Roteadores**: 1 na matriz e 1 por filial, com suporte a interfaces seriais para WAN.
* **Switches Gerenciáveis**: Em cada unidade, com suporte a VLANs e trunking 802.1q.
* **Servidores Locais**: Servidores DNS, DHCP e arquivos em cada unidade.
* **Access Points**: Wireless corporativo distribuído nas filiais com segmentação por SSID/VLAN.

---

## 6. Segurança e Alta Disponibilidade

* A matriz atua como **ponto de autenticação centralizado**, podendo futuramente integrar serviços como RADIUS/LDAP.
* Cada roteador filtra pacotes por ACL, controlando tráfego inter-VLAN e externo.
* Possibilidade de implementação de **failover** via redundância de enlaces e roteadores.
* Todos os dispositivos críticos devem ser monitorados via SNMP/NMS.



