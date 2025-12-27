# ConcurrentApiPlayground

Proyecto demostrativo de **concurrencia, paralelismo y asincronía en C# (.NET)**.

Este repositorio no es un tutorial básico, sino una **colección de escenarios reales**
diseñados para mostrar **cómo funciona el runtime**, cuándo usar cada modelo y
qué problemas resuelve cada uno.

---

## 🧠 Modelo mental (clave del proyecto)

| Concepto          | Qué gestiona                      |
|-------------------|-----------------------------------|
| `async / await`   | Esperas (I/O) sin bloquear        |
| `Parallel`        | CPU y paralelismo real            |
| `ThreadPool`      | Reutilización de hilos            |
| `Task`            | Estado y composición de trabajo   |

> **Async no crea hilos. Parallel no espera I/O.**  
> Entender esta separación es la base del proyecto.

---

## 📦 Escenarios implementados

### 1️⃣ Concurrencia con un solo hilo (async/await)

📌 **Qué demuestra**
- Concurrencia lógica
- Un solo hilo alternando tareas
- No hay paralelismo real

📌 **Casos reales**
- Aplicaciones UI
- Event loops
- Operaciones I/O

📂 Proyecto: Escenario1_Concurrencia1Hilo

---

### 2️⃣ Paralelismo CPU-bound (Parallel)

📌 **Qué demuestra**
- Uso de varios hilos reales
- Trabajo CPU puro
- Código bloqueante intencionalmente

📌 **Casos reales**
- Cálculos matemáticos
- Procesamiento de imágenes
- Compresión / cifrado

📂 Proyecto: Escenario2_ParalelismoCPU

---

### 3️⃣ Concurrencia multihilo (Task + ThreadPool)

📌 **Qué demuestra**
- Tareas no ligadas a un hilo concreto
- ThreadPool como recurso compartido
- I/O asíncrono escalable

📌 **Casos reales**
- Servidores web
- APIs REST
- Microservicios

📂 Proyecto: Escenario3_ConcurrenciaMultihilo

---

### 4️⃣ Paralelismo + asincronía (modelo mixto)

📌 **Qué demuestra**
- CPU paralela
- I/O asíncrono
- Alta escalabilidad sin bloqueos

📌 **Casos reales**
- Pipelines de datos
- Procesamiento masivo
- Servicios de alto rendimiento

📂 Proyecto: Escenario4_ParalelismoAsync

---

## 📊 Resumen comparativo

| Escenario | Hilos | Async  | Paralelo | Caso típico     |
|-----------|-------|--------|----------|-----------------|
| 1         |  1    | ✔️    | ❌       | UI / event loop |
| 2         | >1    | ❌    | ✔️       | CPU-bound       |
| 3         | >1    | ✔️    | ❌       | Backend         |
| 4         | >1    | ✔️    | ✔️       | CPU + I/O       |

---

## ▶️ Cómo ejecutar

Desde la raíz del repositorio:

```bash
dotnet run --project Escenario1_Concurrencia1Hilo
dotnet run --project Escenario2_ParalelismoCPU
dotnet run --project Escenario3_ConcurrenciaMultihilo
dotnet run --project Escenario4_ParalelismoAsync
