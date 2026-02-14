public abstract class SoundType : SoundSettings, ISound
{
    protected const int minVolume = 0;
    protected const int maxVolume = 1;
    protected int onButtonImageIndex;
    protected int offButtonImageIndex;

    public abstract void Execute(SoundController soundController);
}