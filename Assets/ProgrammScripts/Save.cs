using UnityEngine;
using Naninovel;
using System.Threading.Tasks;

public class Save : MonoBehaviour
{
    // Ссылка на компонент Animator
    public Animator animator;

    // Этот метод выполняет сохранение состояния игры и активирует анимацию
    public async void OnButtonClick()
    {
        // Вызов метода анимации
        TriggerAnimation();

        // Выполнение сохранения
        var stateManager = Engine.GetService<IStateManager>();
        await stateManager.QuickSaveAsync();
    }

    // Метод для вызова анимации
    private void TriggerAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Clicker");
        }
        else
        {
            Debug.LogWarning("Animator не назначен.");
        }
    }

    // Метод Update вызывается каждый кадр
    private void Update()
    {
        // Проверяем, была ли нажата клавиша S
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Вызываем метод, как будто нажата кнопка
            OnButtonClick();
        }
    }
}
