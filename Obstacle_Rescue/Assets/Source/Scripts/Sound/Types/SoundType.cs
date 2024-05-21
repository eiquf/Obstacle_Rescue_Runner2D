public abstract class SoundType : SoundSettings, ISound
{
    protected int minVolume;
    protected int maxVolume;
    protected int onButtonImageIndex;
    protected int offButtonImageIndex;

    public abstract void Execute(SoundController soundController);
}