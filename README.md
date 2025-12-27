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

## 💡 Conceptos básicos de Threads y Tasks

### **Thread**
- Unidad de ejecución gestionada por el **sistema operativo**.  
- Ideal para operaciones **CPU-bound largas** o paralelismo real.  
- Control directo del ciclo de vida del hilo, pero más pesado de gestionar.  

### **Task**
- Representa una operación **asincrónica o concurrente**, gestionada por el **ThreadPool de .NET**.  
- Ideal para **operaciones I/O**, como archivos, bases de datos o llamadas a APIs.  
- Maneja **excepciones fácilmente** y permite **reutilización de hilos**.  
- Se integra naturalmente con **async/await**, haciendo el código más limpio y mantenible.

### **Comparativa rápida**

| Concepto | Uso típico | Gestión | Ventaja | Desventaja |
|-----------|-----------|--------|---------|-----------|
| Thread | Operaciones largas / CPU | SO | Control de paralelismo | Pesado, manejo de excepciones difícil |
| Task | I/O, archivos, BD | ThreadPool | Manejo fácil de excepciones, reutilización | Menos control de hilos físicos |

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

## 👀 Qué observar en consola

- **ThreadId**: cada mensaje muestra el `Thread.CurrentThread.ManagedThreadId`.

- **Escenario 1**: un hilo lógico, pero la tarea puede continuar en distintos hilos físicos cuando se completa el `await`.

- **Escenario 2**: cada iteración de `Parallel.For` corre en un hilo físico distinto, paralelismo real.

- **Escenario 3**: `Task.Run` crea hilos del ThreadPool para CPU-bound, y `await` permite I/O concurrente sin bloquear.

- **Escenario 4**: mezcla CPU y I/O; observarás hilos distintos para CPU y continuaciones de I/O.

### Orden de salida

- **Escenario 1**: secuencial o concurrente lógico, salida puede parecer intercalada si se usan varios `await`.  
- **Escenarios 2-4**: salida puede ser no secuencial por paralelismo real, lo que es normal y esperado.

### Excepciones

- Con **Task**, se manejan fácilmente usando `try/catch` en `async/await` o `Task.WhenAll`.  
- Con **Thread** puro, necesitarías capturar excepciones manualmente en cada hilo.
