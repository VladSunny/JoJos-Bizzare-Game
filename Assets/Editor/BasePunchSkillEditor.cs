using UnityEditor;
using UnityEngine;

using JJBG.Combat;
using JJBG;

[CustomEditor(typeof(BasePunchSkill))]
public class BasePunchSkillEditor : Editor
{
    #region SerializedProperties
    SerializedProperty _animator;
    SerializedProperty _playerObj;
    SerializedProperty _player;
    SerializedProperty _dynamicHitBox;
    SerializedProperty _rb;
    SerializedProperty _spMovement;
    SerializedProperty _audioManager;

    SerializedProperty _damage;
    SerializedProperty _knockback;
    SerializedProperty _enemyStunDuration;
    SerializedProperty _lunge;
    SerializedProperty _makeRagdoll;
    SerializedProperty _punchDelay;
    SerializedProperty _attackTime;
    SerializedProperty _soundType;
    #endregion

    private CombatType _combatType;

    private void OnEnable() {
        BasePunchSkill punchSkill = (BasePunchSkill)target;

        GameObject parentObject = punchSkill.gameObject.transform.parent.gameObject;

        if (parentObject != null) {
            ISkillController skillController = parentObject.GetComponent<ISkillController>();

            if (skillController != null) {
                _combatType = skillController.GetCombatType();
            }
            else {
                Debug.LogError("Parent object must have ISkillController component");
            }
        }
        else {
            Debug.LogError("Parent object not found");
        }

        _animator = serializedObject.FindProperty("_animator");
        _playerObj = serializedObject.FindProperty("_playerObj");
        _player = serializedObject.FindProperty("_player");
        _dynamicHitBox = serializedObject.FindProperty("_dynamicHitBox");
        _rb = serializedObject.FindProperty("_rb");
        _spMovement = serializedObject.FindProperty("_spMovement");
        _audioManager = serializedObject.FindProperty("_audioManager");

        _damage = serializedObject.FindProperty("_damage");
        _knockback = serializedObject.FindProperty("_knockback");
        _enemyStunDuration = serializedObject.FindProperty("_enemyStunDuration");
        _lunge = serializedObject.FindProperty("_lunge");
        _makeRagdoll = serializedObject.FindProperty("_makeRagdoll");
        _punchDelay = serializedObject.FindProperty("_punchDelay");
        _attackTime = serializedObject.FindProperty("_attackTime");
        _soundType = serializedObject.FindProperty("_soundType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Combat Type", _combatType.ToString());

        EditorGUILayout.PropertyField(_animator);
        EditorGUILayout.PropertyField(_playerObj);
        EditorGUILayout.PropertyField(_player);
        EditorGUILayout.PropertyField(_dynamicHitBox);
        EditorGUILayout.PropertyField(_rb);
        EditorGUILayout.PropertyField(_audioManager);
        if (_combatType == CombatType.Stand)
            EditorGUILayout.PropertyField(_spMovement);

        EditorGUILayout.PropertyField(_damage);
        EditorGUILayout.PropertyField(_knockback);
        EditorGUILayout.PropertyField(_enemyStunDuration);
        EditorGUILayout.PropertyField(_lunge);
        EditorGUILayout.PropertyField(_makeRagdoll);
        EditorGUILayout.PropertyField(_punchDelay);
        EditorGUILayout.PropertyField(_soundType);
        if (_combatType == CombatType.Stand)
            EditorGUILayout.PropertyField(_attackTime);

        serializedObject.ApplyModifiedProperties();
    }
}

