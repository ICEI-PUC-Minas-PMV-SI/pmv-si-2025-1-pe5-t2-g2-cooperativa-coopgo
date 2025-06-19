## VIRTUALIZAÇÃO E CLOUD
Esta é marcada pelo mapeamento e implantação dos servidores em nuvem e on-premise para o devido atendimento do planejamento inicial. 

## Ambiente VM
O ambiente selecionado foi o Windows Server 2012, no qual realizamos a configuração do servidor, incluindo a criação de diretórios, o gerenciamento de usuários e a aplicação de políticas de grupo (GPO).O nome atribuído ao servidor foi Server01 e o domínio configurado é coopgo.net.

**Unidades Organizacionais**

Dentro do domínio, foram criadas unidades organizacionais específicas para melhor organização da Matriz e as filiais com usuários e computadores.

**Gerenciamento de Política de Grupo**

A título de exemplo foi criada uma política que bloqueia o acesso ao Painel de Controle e às configurações do computador para os usuários desta unidade organizacional específica

## Ambiente AWS

Configuramos a rede VPC na AWS, criamos as zonas de disponibilidade e estabelecemos as sub-redes. A VPC com ID vpc-0820bbeceee8b032b é nomeada como Matriz-vpc e está no estado Available, o que significa que está disponível para uso. Ela está localizada na região us-east-1 (Norte da Virgínia) e foi configurada com o bloco CIDR 10.0.0.0/16 

**Grupo de Segurança: Matriz**


**Regras de Entrada (Inbound Rules)**

As regras de entrada definem quais tipos de tráfego de rede podem acessar as instâncias EC2 associadas a este grupo de segurança. Abaixo está o detalhamento das regras ativas:

| Tipo                    | Protocolo | Intervalo de Portas | Descrição |
|-------------------------|-----------|----------------------|-----------|
| Todos os ICMPs - IPv4   | ICMP      | Tudo                 | Permite tráfego ICMP, como ping e traceroute, utilizado para testes de conectividade. |
| UDP Personalizado       | UDP       | 161 - 162            | Permite tráfego via protocolo SNMP (usado para monitoramento de rede). |
| RDP                     | TCP       | 3389                 | Libera acesso remoto via Remote Desktop Protocol, comum em servidores Windows. |
| HTTP                    | TCP       | 80                   | Libera tráfego HTTP (web sem criptografia), utilizado para acesso a páginas web. |

---

**Considerações de Segurança**

- A liberação da porta **3389 (RDP)** deve ser feita com cautela, preferencialmente restringindo o acesso a IPs específicos ou utilizando uma VPN.
- A permissão de tráfego **HTTP (porta 80)** é comum em aplicações web, mas recomenda-se o uso de HTTPS (porta 443) para maior segurança.
- **ICMP** é útil para diagnóstico, mas pode ser explorado em alguns tipos de ataques de rede; recomenda-se permitir apenas se necessário.
- O uso de **SNMP (portas 161–162)** deve ser monitorado e devidamente autenticado para evitar exposições.


**Instância**

Criamos o servidor EC2, denominado MatrizServer, dentro da VPC configurada. A imagem utilizada foi o Windows Server 2016, e o tipo de instância escolhido foi t2.large. O servidor foi associado ao grupo de segurança previamente criado



