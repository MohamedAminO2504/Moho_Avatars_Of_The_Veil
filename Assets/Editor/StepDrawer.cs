using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Step))]
public class StepDrawer : PropertyDrawer
{
    private const float SEPARATOR_HEIGHT = 8f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float y = position.y;
        float line = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        void DrawLine(string relativeName)
        {
            var p = property.FindPropertyRelative(relativeName);
            var r = new Rect(position.x, y, position.width, line);
            EditorGUI.PropertyField(r, p);
            y += line + spacing;
        }

        void DrawAny(string relativeName, bool includeChildren)
        {
            var p = property.FindPropertyRelative(relativeName);
            float h = EditorGUI.GetPropertyHeight(p, includeChildren);
            var r = new Rect(position.x, y, position.width, h);
            EditorGUI.PropertyField(r, p, includeChildren);
            y += h + spacing;
        }

        // Read properties
        var typeProp = property.FindPropertyRelative("type");
        var actorProp = property.FindPropertyRelative("actor");
        var stateProp = property.FindPropertyRelative("state");
        var lookProp = property.FindPropertyRelative("look");

        StepType stepType = (StepType)typeProp.enumValueIndex;
        StepState state = (StepState)stateProp.enumValueIndex;
        LookType look = (LookType)lookProp.enumValueIndex;
        Card actor = actorProp.objectReferenceValue as Card;

        // TYPE (always)
        DrawLine("type");

        if (ShowLook(stepType, look))         DrawLine("look");
        if (ShowState(stepType, look))        DrawLine("state");
        if (ShowActor(stepType, look))        DrawLine("actor");
        if (ShowLocation(stepType))           DrawLine("location");
        if (ShowActor2(stepType, look))       DrawLine("actor2");
        if (ShowScene(stepType))              DrawLine("scene");
        if (ShowNewType(stepType))            DrawLine("typeCard");
        if (ShowVariable(stepType, state))    DrawLine("variable");

        if (actor != null && ShowCara(stepType, actor))
            DrawAny("cara", true);

        if (ShowDialogue(stepType, state))
            DrawAny("dialogue", true);

        // ───────────── BATTLE ─────────────
        if (ShowBattle(stepType))
        {
            DrawLine("battleType");
            DrawAny("battleParam", true);
        }

        // ───────────── Separator ─────────────
        float separatorY = y + 2f;
        Rect separatorRect = new Rect(position.x, separatorY, position.width, 1f);

        EditorGUI.DrawRect(
            separatorRect,
            EditorGUIUtility.isProSkin
                ? new Color(0.2f, 0.2f, 0.2f, 1f)
                : new Color(0.7f, 0.7f, 0.7f, 1f)
        );

        y += SEPARATOR_HEIGHT;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0f;
        float line = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        void AddLine() => height += line + spacing;

        void AddAny(string relativeName, bool includeChildren)
        {
            var p = property.FindPropertyRelative(relativeName);
            height += EditorGUI.GetPropertyHeight(p, includeChildren) + spacing;
        }

        var typeProp = property.FindPropertyRelative("type");
        var actorProp = property.FindPropertyRelative("actor");
        var stateProp = property.FindPropertyRelative("state");
        var lookProp = property.FindPropertyRelative("look");

        StepType stepType = (StepType)typeProp.enumValueIndex;
        StepState state = (StepState)stateProp.enumValueIndex;
        LookType look = (LookType)lookProp.enumValueIndex;
        Card actor = actorProp.objectReferenceValue as Card;

        // TYPE (always)
        AddLine();

        if (ShowLook(stepType, look))         AddLine();
        if (ShowState(stepType, look))        AddLine();
        if (ShowActor(stepType, look))        AddLine();
        if (ShowLocation(stepType))           AddLine();
        if (ShowActor2(stepType, look))       AddLine();
        if (ShowScene(stepType))              AddLine();
        if (ShowNewType(stepType))            AddLine();
        if (ShowVariable(stepType, state))    AddLine();

        if (actor != null && ShowCara(stepType, actor))
            AddAny("cara", true);

        if (ShowDialogue(stepType, state))
            AddAny("dialogue", true);

        // ───────────── BATTLE ─────────────
        if (ShowBattle(stepType))
        {
            AddLine(); // battleType
            AddAny("battleParam", true);
        }

        // Separator height
        height += SEPARATOR_HEIGHT;

        return height;
    }

    private bool ShowBattle(StepType type)
    {
        return type == StepType.BATTLE;
    }

    private bool ShowLocation(StepType type)
    {
        return type == StepType.POSE
            || type == StepType.CHANGE_TYPE;
    }

    private bool ShowNewType(StepType type)
    {
        return type == StepType.CHANGE_TYPE;
    }

    private bool ShowState(StepType type, LookType look)
    {
        return type == StepType.INFO
            || type == StepType.VARIABLE
            || type == StepType.DONJON
            || type == StepType.DIALOGUE
            || type == StepType.WAIT;
    }

    private bool ShowLook(StepType type, LookType look)
    {
        return type == StepType.LOOK;
    }

    private bool ShowScene(StepType type)
    {
        return type == StepType.CHANGE_SCENE;
    }

    private bool ShowVariable(StepType type, StepState state)
    {
        return type == StepType.VARIABLE
            || (type == StepType.WAIT && state == StepState.WAIT);
    }

    private bool ShowCara(StepType type, Card actor)
    {
        return type == StepType.POSE && actor.type == TypeCard.BESTIAIRE;
    }

    private bool ShowActor(StepType type, LookType look)
    {
        return type == StepType.PERSO_MOVE
            || type == StepType.CHANGE_TYPE
            || type == StepType.WAIT_LIEU
            || type == StepType.DIALOGUE
            || type == StepType.ACTIVE_CARD
            || type == StepType.DESACTIVE_CARD
            || type == StepType.DONJON
            || type == StepType.POSE
            || (type == StepType.LOOK && look == LookType.ALL_CARD)
            || (type == StepType.LOOK && look == LookType.ALL_CARD_WITH_BODY)
            || (type == StepType.LOOK && look == LookType.X_TO_Y);
    }

    private bool ShowActor2(StepType type, LookType look)
    {
        return type == StepType.PERSO_MOVE
            || (type == StepType.LOOK && look == LookType.X_TO_Y);
    }

    private bool ShowDialogue(StepType type, StepState state)
    {
        return type == StepType.DIALOGUE
            || (type == StepType.INFO && state == StepState.START);
    }
}