using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSelector : MonoBehaviour
{

		// Agrega otras resoluciones si lo deseas

		//RESOLUCIONES PARA STEAM DECK
//		private Resolution[] supportedResolutions = new Resolution[] {
//		new Resolution() { width = 640, height = 480 },  //(4:·3)
//		new Resolution() { width = 960, height = 720 }, //(4:·3)
//		new Resolution() { width = 1280, height = 720 },
//		new Resolution() { width = 1066, height = 800 }, //(4:·3)
//		new Resolution() { width = 1280, height = 800 },
//		new Resolution() { width = 1440, height = 1080 }, //(4:·3)
//		new Resolution() { width = 1920, height = 1080 }


		// Define las resoluciones soportadas por el juego para todas las plataformas (MAC WINDOWS LINUX)
//		private Resolution[] supportedResolutions = new Resolution[] {
//			new Resolution() { width = 640, height = 480 },
//			new Resolution() { width = 960, height = 540 },
//			new Resolution() { width = 960, height = 720 },
//			new Resolution() { width = 1280, height = 720 },
//			new Resolution() { width = 1066, height = 800 },
//			new Resolution() { width = 1280, height = 800 },
//			new Resolution() { width = 1440, height = 900 },
//			new Resolution() { width = 1440, height = 1080 },
//			new Resolution() { width = 1920, height = 1080 },
//			new Resolution() { width = 2560, height = 1080 },
//			new Resolution() { width = 2880, height = 2160 },
//			new Resolution() { width = 3840, height = 2160 }
	
	[Header("UI ELEMENTS")]
	public Dropdown dropdown;
	public Text resolutionText;
	//public GameObject warningPanel;
	//public Button continueButton;
	public bool isFirstRun = true;
	
	[Header("RESOLUTIONS UTILS")]
	public bool displayChanged = false;
	public List<Resolution> currentResolutions;
	public int currentResolutionIndex;
	public Resolution currentMaxResolution;
    
	public Resolution lastKnownResolution;
	public int lastKnownDisplayCount;

// Define las resoluciones soportadas por el juego para todas las plataformas (MAC WINDOWS LINUX)
	private Resolution[] supportedResolutions = new Resolution[] {
		new Resolution() { width = 640, height = 480 },
		new Resolution() { width = 960, height = 540 },
		new Resolution() { width = 960, height = 720 },
		new Resolution() { width = 1280, height = 720 },
		new Resolution() { width = 1366, height = 768 },
		new Resolution() { width = 1066, height = 800 },
		new Resolution() { width = 1280, height = 800 },
		new Resolution() { width = 1440, height = 900 },
		new Resolution() { width = 1440, height = 1080 },
		new Resolution() { width = 1920, height = 1080 },
		new Resolution() { width = 2560, height = 1080 },
		new Resolution() { width = 2880, height = 2160 },
		new Resolution() { width = 3840, height = 2160 }
	};
		
	void Start()
	{
		// Forzar una actualización de la información de las pantallas
		lastKnownResolution = Screen.currentResolution;
		lastKnownDisplayCount = Display.displays.Length;
		RefreshDisplayInfo();
		// Actualizar opciones del dropdown
		UpdateDropdownOptions();
		// Cargar ultima opcion Playerprefb
		LoadSavedResolution();

		// -----------------PANEL ADVERTENCIA----------------------------
		//warningPanel.SetActive(false); 		// Ocultar el panel de advertencia al inicio
		//continueButton.onClick.AddListener(OnContinueButtonClicked); 		// Agregar listener al botón continuar
		// --------------------------------------------------------------

		isFirstRun = true;
		StartCoroutine(CheckForDisplayChanges());
	}

	#region CORRUTINA PARA VERIFICAR SI HAY CAMBIO DE PANTALLA CADA 2 SEGUNDOS
	// Compara la resolución y la cantidad de displays actuales con la última conocida para determinar si ha habido un cambio.
	IEnumerator CheckForDisplayChanges()
	{
		while (true)
		{
			yield return new WaitForSeconds(2f);
			if (HasDisplayConfigurationChanged())
			{
				if (!isFirstRun)
				{
					Debug.Log("Cambio de pantalla detectado en la corrutina.");
					OnDisplaysUpdated();
				}
				else
				{
					isFirstRun = false;
				}
			}
		}
	}
	bool HasDisplayConfigurationChanged()
	{
		Resolution currentResolution = Screen.currentResolution;
		int currentDisplayCount = Display.displays.Length;

		bool resolutionChanged = currentResolution.width != lastKnownResolution.width ||
		                         currentResolution.height != lastKnownResolution.height ||
		                         currentResolution.refreshRate != lastKnownResolution.refreshRate;

		bool displayCountChanged = currentDisplayCount != lastKnownDisplayCount;

		if (resolutionChanged || displayCountChanged)
		{
			Debug.Log($"Cambio detectado - Resolución anterior: {lastKnownResolution.width}x{lastKnownResolution.height}@{lastKnownResolution.refreshRate}Hz, " +
			          $"Nueva resolución: {currentResolution.width}x{currentResolution.height}@{currentResolution.refreshRate}Hz");
			Debug.Log($"Monitores antes: {lastKnownDisplayCount}, Monitores ahora: {currentDisplayCount}");
			return true;
		}
		return false;
	}
	#endregion
	
	#region REFRESCAR/ACTUALIZAR INFORMACION DE RESOLUCION Y DROPDOWN
	void LoadSavedResolution()
	{
		currentResolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
		if (currentResolutionIndex < currentResolutions.Count)
		{
			Resolution savedResolution = currentResolutions[currentResolutionIndex];
			SetResolution(savedResolution.width, savedResolution.height);
			dropdown.value = currentResolutionIndex;
			Debug.Log("Resolucion cargada: " + savedResolution );
		}
		else
		{
			Debug.LogWarning("Índice de resolución guardado no válido. Usando la resolución por defecto.");
			currentResolutionIndex = 0;
			PlayerPrefs.SetInt("resolutionIndex", currentResolutionIndex);
		}
	}

	void RefreshDisplayInfo()
	{
		// Fuerza una actualización de la información de pantalla.
		Debug.Log("Actualizando información de pantalla.");

		// Forzar una actualización de la información de pantalla
		Screen.fullScreen = !Screen.fullScreen;
		Screen.fullScreen = !Screen.fullScreen;
	}
	public void ForceRefresh() // PARA CAMBIO DE PANTALLA /HDMI/DISPLAYPORT/ETC
	{
		// Fuerza una actualización de la información de pantalla y opciones del dropdown.
		Debug.Log("Forzando actualización.");
		RefreshDisplayInfo();
		UpdateDropdownOptions();
		displayChanged = false;
		lastKnownResolution = Screen.currentResolution;
		lastKnownDisplayCount = Display.displays.Length;
	}
	private void SetResolution(int width, int height)
	{
		if (!Screen.fullScreen)
		{
			// En modo ventana, ajustamos el tamaño de la ventana
			Screen.SetResolution(width, height, false);
			// Forzamos una actualización del tamaño de la ventana
			#if UNITY_STANDALONE
			Screen.fullScreen = true;
			Screen.fullScreen = false;
			#endif
			// Este bloque de código solo se compila y ejecuta en plataformas
			// standalone (como Windows, macOS o Linux). Primero, se cambia temporalmente
			// el modo a pantalla completa (Screen.fullScreen = true), y luego se vuelve a
			// cambiar a modo ventana (Screen.fullScreen = false). Esto fuerza una actualización del tamaño de la ventana.
		}
		else
		{
			Screen.SetResolution(width, height, true);
		}
		Debug.Log($"Resolución establecida a: {width}x{height}, Pantalla completa: {Screen.fullScreen}");
	}
	void UpdateDropdownOptions()
	{
		currentMaxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
		Debug.Log($"Resolución máxima disponible: {currentMaxResolution.width}x{currentMaxResolution.height}");

		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		currentResolutions = new List<Resolution>();

		foreach (Resolution res in supportedResolutions)
		{
			if (res.width <= currentMaxResolution.width && res.height <= currentMaxResolution.height)
			{
				string optionText = GetResolutionString(res);
				options.Add(new Dropdown.OptionData(optionText));
				currentResolutions.Add(res);
			}
		}
        
		Debug.Log($"Resolución máxima disponible: {currentMaxResolution.width}x{currentMaxResolution.height}");
	
		dropdown.ClearOptions();
		dropdown.AddOptions(options);

		// Intentar restaurar la resolución previamente seleccionada
		currentResolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
		currentResolutionIndex = Mathf.Clamp(currentResolutionIndex, 0, options.Count - 1);
		dropdown.value = currentResolutionIndex;
		dropdown.RefreshShownValue();
		
		// Comentar o eliminar esta línea para evitar cambiar la resolución aquí
		//OnDropdownValueChanged(currentResolutionIndex);
	}
	#endregion

	#region DROPDOWN RESOLUTION UTILITY
	// ASIGNAR ONDROPDOWNVALUECHANGED() AL METODO DEL DROPDOWN On Value Changed ()
	public void OnDropdownValueChanged(int index)
	{
		if (currentResolutions != null && index < currentResolutions.Count)
		{
			currentResolutionIndex = index;
			PlayerPrefs.SetInt("resolutionIndex", currentResolutionIndex);
			PlayerPrefs.Save();

			Resolution selectedResolution = currentResolutions[index];
			SetResolution(selectedResolution.width, selectedResolution.height);
            
			Debug.Log($"Cambiando resolución a: {selectedResolution.width}x{selectedResolution.height}");
		}
		else
		{
			Debug.LogError($"Índice inválido o currentResolutions es null. Index: {index}, Count: {currentResolutions?.Count ?? 0}");
		}
	}

	// LISTA DE RESOLUCIONES PARA WINDOWS / MAC PARA AGREGAR AL DROPDOWN
	private string GetResolutionString(Resolution resolution)
	{
		string aspectRatio = "";
		if (resolution.width == 640 && resolution.height == 480) aspectRatio = " (4:3)";
		else if (resolution.width == 960 && resolution.height == 540) aspectRatio = " (16:9)";
		else if (resolution.width == 960 && resolution.height == 720) aspectRatio = " (4:3)";
		else if (resolution.width == 1280 && resolution.height == 720) aspectRatio = " (16:9)";
		else if (resolution.width == 1366 && resolution.height == 768) aspectRatio = " (16:9)";
		else if (resolution.width == 1066 && resolution.height == 800) aspectRatio = " (4:3)";
		else if (resolution.width == 1280 && resolution.height == 800) aspectRatio = " (16:10)";
		else if (resolution.width == 1440 && resolution.height == 900) aspectRatio = " (16:10)";
		else if (resolution.width == 1440 && resolution.height == 1080) aspectRatio = " (4:3)";
		else if (resolution.width == 1920 && resolution.height == 1080) aspectRatio = " (16:9)";
		else if (resolution.width == 2560 && resolution.height == 1080) aspectRatio = " (21:9)";
		else if (resolution.width == 2880 && resolution.height == 2160) aspectRatio = " (4:3)";
		else if (resolution.width == 3840 && resolution.height == 2160) aspectRatio = " (4K)";

		return $"{resolution.width} x {resolution.height}{aspectRatio}";
	}
	#endregion

	#region LOGICA PARA CUANDO SE DETECTA NUEVO CAMBIO DE PANTALLA
	
	#region CALLBACK EVENTOS DE CAMBIO DE PANTALLA
	void OnDisplaysUpdated()
	{
		Debug.Log("Detectado cambio en las pantallas. Actualizando opciones de resolución.");
		displayChanged = true;
		//ShowWarningPanel();
		UpdateDropdownOptions();
	}
	void OnEnable()
	{
		Display.onDisplaysUpdated += OnDisplaysUpdated;
	}

	void OnDisable()
	{
		Display.onDisplaysUpdated -= OnDisplaysUpdated;
	}
	#endregion
	#region PANEL ADVERTENCIA
	/*void OnContinueButtonClicked()
	{
		// Oculta el panel de advertencia y, si hubo un cambio en la configuración de pantalla, fuerza una actualización.
		Debug.Log("Botón Continuar presionado.");
		if (displayChanged)
		{
			ForceRefresh();
		}
		warningPanel.SetActive(false);
	}
	void ShowWarningPanel()
	{
		Debug.Log("Mostrando panel de advertencia.");
		warningPanel.SetActive(true);
	}*/
	#endregion
	
	#endregion
}


