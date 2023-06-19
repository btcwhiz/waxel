using System;

[Serializable]
public class ConfigDataModel 
{
    public DelayDataModel[] race_delay_values;
    public DelayDataModel[] rawmat_refined;
    public DelayDataModel[] craft_delays;
    public DelayDataModel[] template_ids;
    public ProfessionComboDataModel[] profession_combo;
}
