
# 🔊 Unity Speaker Mode Selector

[![Unity Version](https://img.shields.io/badge/Unity-2021.3.15f1%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![UnityPackage](https://img.shields.io/badge/Package-SpeakerModeTool%2B-blue.svg)](https://github.com/KaitoOwO/DropdownSpeakerMode/blob/main/SpeakerModeTool.unitypackage
)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.md)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux%20%7C%20Xbox%20%7C%20PlayStation-lightgrey)](#)
[![Contributions](https://img.shields.io/badge/Contributions-Welcome-orange.svg)](CONTRIBUTING.md)
[![Twitter Follow](https://img.shields.io/twitter/follow/kaitoartzz?style=social)](https://x.com/kaitoartzz)

> Una herramienta eficiente y fácil de integrar para seleccionar y gestionar modos de altavoz en proyectos Unity, compatible con múltiples plataformas.

---
✨🌷 ¡Hola, pequeños devs!💖 Soy **KaitoArtz**, y estoy emocionado de compartir este proyecto contigo.

Esta guía te proporcionará instrucciones detalladas para integrar, personalizar y modificar la herramienta de selección de modo de speaker en un proyecto de Unity. Este package fue desarrollado en Unity **2021.3.15f1** y contiene varias escenas preconfiguradas, scripts y assets necesarios para su funcionamiento.

GUÍA DE USO / DOCS:

[![GoogleDocs](https://img.shields.io/badge/Guide-GoogleDocs-blue?style=for-the-badge)](https://docs.google.com/document/d/1fNBMMr-iCRFTAYUThmCsrDRbVIwLbtT8DEUj7X-9amA/edit?usp=sharing)



![Speaker Mode Selector Demo](.image/speakerdemo.gif)

---

## 📖 Tabla de Contenidos

- [✨ Características](#-características)
- [🔧 Requisitos Previos](#-requisitos-previos)
- [📦 Instalación](#-instalación)
- [⚙️ Configuración](#️-configuración)
- [🚀 Uso](#-uso)
- [🎮 Pruebas](#-pruebas)
- [📚 Documentación Detallada](#-documentación-detallada)
- [🤝 Contribuciones](#-contribuciones)
- [📄 Licencia](#-licencia)
- [📞 Soporte](#-soporte)

---

## ✨ Características

- **Compatibilidad Multiplataforma**: Funciona en Windows, macOS, Linux, Xbox y Playstation.
- **Selección Dinámica**: Permite cambiar entre diferentes modos de altavoz en tiempo real.
- **Interfaz de Usuario Intuitiva**: Utiliza un dropdown para una selección fácil y rápida.
- **Persistencia de Configuración**: Guarda y carga automáticamente la última configuración de audio seleccionada.
- **Transiciones Suaves**: Implementa cambios de modo de altavoz con transiciones graduales para una mejor experiencia de usuario.
- **Fácil Integración**: Diseñado para ser incorporado rápidamente en proyectos Unity existentes.
  
> [!TIP]
> **¿Sabías que...?** Esta herramienta también puede adaptarse para agregar mas modos de parlante.

---
## 🛠 Requisitos Previos

- Unity versión 2021.3.15f1 o superior.
- Conocimientos básicos de Unity y C#.
- Familiaridad con el sistema de audio de Unity y AudioMixer.
- Manejo de Canvas de Unity.
---

## 📦 Instalación

### Requisitos Previos

- **Unity** versión **2021.3.15f1** o superior.
- Conocimiento básico de la interfaz de Unity y manejo de scripts.
- Package: [![UnityPackage](https://img.shields.io/badge/Package-SpeakerModeTool%2B-blue.svg)](https://github.com/KaitoOwO/DropdownSpeakerMode/blob/main/SpeakerModeTool.unitypackage
)

### Pasos de Instalación

1. **Importar el Package**
   - Abre tu proyecto Unity (2021.3.15f1 o superior).
   - Ve a `Assets > Import Package > Custom Package`.
   - Selecciona el package de la herramienta de selección de speaker descargado.
   - Contenido:

     ![image](https://github.com/user-attachments/assets/aaeb0bef-5f65-4500-a1ec-c7123c8e53e1)

2. **Configuración de Escenas**
   - Abre `File > Build Settings`.
   - Asegúrate de que la escena "LoadInitialization" sea la primera en la lista.
   - Ordena las demás escenas según las necesidades de tu proyecto.
     
     ![image](https://github.com/user-attachments/assets/413fd8c2-6918-469c-9115-5365267538b8)

3. **Verificación de Componentes**
   - Revisa que los siguientes scripts estén presentes en tu proyecto:
     - `AudioInitializer`
     - `AudioSettingsManager`
     - `AudioSettingsDropdown`
     - `SceneManager`
     - `ChangeSceneTimer`
       
    ![image](https://github.com/user-attachments/assets/51471009-4ca3-42bd-acbb-e73dc2ac5e19)



> [!NOTE]
> Puedes testear y probar las escenas para ver en tiempo real como funcionan los scripts y sus funcionalidades.

---

## ⚙️ Configuración

### AudioSettingsManager

Este componente gestiona los modos de altavoz disponibles y las transiciones entre ellos.

```csharp
public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance { get; private set; }
    
    [SerializeField] private AudioMixer audioMixer;

    public AudioSpeakerMode[] speakerModes = {
        AudioSpeakerMode.Mono,
        AudioSpeakerMode.Stereo,
        AudioSpeakerMode.Quad,
        AudioSpeakerMode.Mode5point1,
    };

    // ... (resto del código)
}
```

### AudioSettingsDropdown

Maneja la interfaz de usuario para la selección de modos de altavoz.

```csharp
public class AudioSettingsDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown speakerModeDropdown;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button cancelButton;

    // ... (resto del código)
}
```

> [!WARNING]
> Asegúrate de estar utilizando la version actualizada del `TextMeshPro` de Unity.

### Configuración del Inspector

- **Dropdown**: Asigna el prefab `SpeakerDropdown` al script `AudioSettingsDropdown`.
- **Warning Panel**: Asigna el prefab `WarningPanel` que se mostrará cuando se detecten cambios en las pantallas.
- **Continue Button**: `Botón` para confirmar y aplicar cambios tras detectar un nuevo cambio de parlante.
- **Cancel Button**: `Botón` para cancelar y no aplicar los cambios.

---

## 🚀 Uso

1. **Inicialización**
   - La escena "LoadInitialization" se carga primero, inicializando la configuración de audio.
   - Después de un breve retraso, se carga la escena principal.

2. **Selección de Modo de Altavoz**
   - En la escena principal, utiliza el dropdown para seleccionar el modo de altavoz deseado.
   - Los cambios se aplican inmediatamente con una transición suave.

3. **Manejo de Advertencias**
   - Si se requiere un reinicio para aplicar los cambios, se mostrará un panel de advertencia.
   - Utiliza los botones "Apply" o "Cancel" según sea necesario.

4. **Navegación entre Escenas**
   - Utiliza el `SceneManager` para moverte entre diferentes escenas manteniendo la configuración de audio.

> [!TIP]
> Para manejar el buffer de audio de manera óptima cuando se cambian modos de speaker, puedes modificar el `AudioSettingsManager` para pausar y reanudar el audio de forma más controlada, evitando problemas de corte abrupto.

---

## 🎮 Pruebas

### **Cambio de speaker en tiempo real**

### **MainScene:**

Una vez que estemos en game. Cargará la escena **LoadInitialization** y pasará a **MainScene**. 

![image](https://github.com/user-attachments/assets/352a190b-5cb0-45fa-b401-8826eb4dc243)

Si cambiamos el tipo de speaker, aparecerá el panel de advertencia.

#### **Panel de Advertencia:**

![image](https://github.com/user-attachments/assets/666c83fe-784c-4050-ad71-c6844445b3ce)

Si le damos a **Aplicar**, se pausara el audio, pero luego volverá a reanudar normalmente (Esto es como feedback de que el cambio se realizó correctamente) y se vera asi el **AudioMixer**:

#### **Cambios en AudioMixer:**

![image](https://github.com/user-attachments/assets/0b778078-887c-4c2a-aef0-d5116761c3e6)
![image](https://github.com/user-attachments/assets/c203bc46-dbb4-4a14-8388-0c610536a1a7)

### **Comprobar cambios:**

Una vez que queramos cambiar de escena, nos fijamos que el **AudioMixer** sigue con el mismo valor aplicado.

### **Escena de nivel:**

![image](https://github.com/user-attachments/assets/f503095c-22cb-48fa-ac66-960fc29a8df8)

Vemos que la escena de Nivel, es como cuando le damos Jugar a nuestro videojuego. Como ven, el audiomixer sigue con el mismo tipo de parlante.

### **Escena de pausa:**

Ahora como también estuvieramos testeando nuestro videojuego. Si vamos a la escena de pausa o vamos a una escena que también contenga un panel de opciones con el **Dropdown** de cambiar el parlante (**Speaker**), también guardará los valores que teníamos en la escena principal.

![image](https://github.com/user-attachments/assets/069aa923-547c-4ca4-b426-11779f9f767c)

### **Cerrar y volver a abrir el juego:**

Si cerramos nuestro videojuego, y luego lo volviéramos a abrir, nuestra escena de **LoadInitialization**, hará todo el trabajo para cargar los datos del playerprefb que guardamos con el valor del tipo de parlante.

![image](https://github.com/user-attachments/assets/d17d5ad6-e2e2-444d-aeb7-f2a77001764f)

Esperamos un momento y…

![image](https://github.com/user-attachments/assets/1f625439-d55b-4a59-9222-6ee405cbb607)

---

## 📚 Documentación Detallada

### Adaptación a Otros Proyectos

#### Modificar Modos de Altavoz Disponibles

En `AudioSettingsManager`, ajusta el array `speakerModes`:

```csharp
public AudioSpeakerMode[] speakerModes = {
    AudioSpeakerMode.Mono,
    AudioSpeakerMode.Stereo,
    AudioSpeakerMode.Quad,
    AudioSpeakerMode.Mode5point1,
    AudioSpeakerMode.Mode7point1 // Añadir si es necesario
};
```

#### Personalizar la Interfaz de Usuario

En `AudioSettingsDropdown`, modifica `GetDisplayNameForSpeakerMode`:

```csharp
private string GetDisplayNameForSpeakerMode(AudioSpeakerMode mode)
{
    switch (mode)
    {
        case AudioSpeakerMode.Stereo: return "Estéreo";
        case AudioSpeakerMode.Quad: return "Cuadrafónico";
        case AudioSpeakerMode.Mode5point1: return "Surround 5.1";
        // Añadir más casos según sea necesario
        default: return mode.ToString();
    }
}
```

#### Ajustar el SceneManager

Personaliza el `SceneManager` para manejar la navegación entre escenas según tu estructura de proyecto:

```csharp
public class SceneManager : MonoBehaviour
{
    [SerializeField] public string mainSceneName = "MainScene";
    [SerializeField] public string[] levelNames;

    public void LoadNextLevel(int currentLevel)
    {
        if (currentLevel < levelNames.Length - 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelNames[currentLevel + 1]);
        }
        else
        {
            Debug.Log("No hay más niveles disponibles");
        }
    }

    // Añadir más métodos según sea necesario
}
```

> [!NOTE]
> Consulta el código fuente completo para más detalles y comentarios que te ayudarán a entender y modificar el comportamiento según tus necesidades.

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, sigue estos pasos:

1. Haz un Fork del repositorio.
2. Crea una rama nueva: `git checkout -b feature/nueva-funcionalidad`.
3. Realiza tus cambios y haz commit: `git commit -m 'Agrega nueva funcionalidad'`.
4. Haz push a la rama: `git push origin feature/nueva-funcionalidad`.
5. Abre un Pull Request.

> [!TIP]
> Asegúrate de seguir las convenciones de codificación y agregar comentarios claros para facilitar la revisión.

---

## 📄 Licencia

Este proyecto está licenciado bajo la licencia MIT. Esto significa que eres libre de usar, modificar y distribuir este software de acuerdo con los términos de la licencia.
Consulta el archivo [Licencia](LICENSE.md) para más detalles.

---

## 🔰 Políticas de Privacidad

Este proyecto no recopila información personal ni técnica de los usuarios. Para más detalles, consulta nuestras [Políticas de Privacidad](SECURITY.md).

---

## 🔗 Enlaces de Interés

- **Portfolio Personal**: [![Twitter Follow](https://img.shields.io/twitter/follow/kaitoartzz?style=social)](https://x.com/kaitoartzz)
- **Itch.io / Videojuegos Publicados**: [![Itch.io](https://img.shields.io/badge/KaitoArtz-%23FF0B34.svg?logo=Itch.io&logoColor=white)](https://kaitoartz.itch.io)
- **Otros Proyectos en Unity**: [![GitHub](https://img.shields.io/badge/KaitoArtz-%23121011.svg?logo=github&logoColor=white)](https://github.com/KaitoOwO)

---

## 📞 Soporte

Si tienes preguntas, problemas o sugerencias, no dudes en contactarme:

- **Email**: kaitoartz.info@gmail.com
- **Twitter**: [@kaitoartzz](https://twitter.com/K41t0M)
- **Discord**: kaitoowo



Espero que les guste esta herramienta que la cree y programe para un proyecto universitario. La estuve puliendo y trabajando para que se pudiera integrar fácil en cualquier proyecto y para compartirlo con ustedes! Si gustan, dejen una estrella, comenten, o si encuentran alguna idea para mejorar, no olviden dejar su [![GitHub Pages](https://img.shields.io/badge/%20Issues-121013?logo=github&logoColor=white)](https://github.com/KaitoOwO/DropdownResolution/issues) y con gusto mejoraremos juntos esta herramienta. ✨❤️

---
Desarrollado con ❤️ por *KaitoArtz*
