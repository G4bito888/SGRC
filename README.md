# SGRC - Sistema de Gestión de Recursos Comunitarios
Hecho por: Eric Charpenel Martínez, Ethan Gabriel Covarrubias Gutiérrez y Alejandro Hernández Ramos.

## Descripción del Proyecto
El Sistema de Gestión de Recursos Comunitarios (SGRC) es un software de escritorio diseñado para optimizar el inventario y la logística en bancos de alimentos locales. Su enfoque principal es ofrecer una interfaz intuitiva para que el personal voluntario pueda gestionar recursos perecederos y entregas de forma eficiente, sin requerir experiencia técnica previa.

## Objetivo
Nuestro objetivo principal es centralizar y optimizar la gestión de recursos en organizaciones de asistencia social. Buscamos reducir el desperdicio operativo y alimentario para asegurar una distribución eficaz, alineando directamente nuestro impacto con los ODS 2 (Hambre Cero) y ODS 12 (Consumo y Producción Responsables).

## Guía de Inicio Rápido (Entorno de Desarrollo)
Este proyecto utiliza *Docker* para orquestar las bases de datos. No es necesario instalar Oracle o Neo4j localmente.

### Requisitos
1. Tener instalado [Docker Desktop](https://www.docker.com/products/docker-desktop/).
2. .NET 10 SDK.

### Levantar Infraestructura
Ejecuta el siguiente comando en la raíz del proyecto:
```bash
docker compose up -d