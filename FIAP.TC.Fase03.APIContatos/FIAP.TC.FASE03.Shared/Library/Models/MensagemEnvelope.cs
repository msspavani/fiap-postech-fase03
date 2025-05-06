namespace FIAP.TC.FASE03.Shared.Library.Models;

public class MensagemEnvelope
{
    public object Payload { get; set; } = default!;
}

public class MensagemEnvelopeCreate : MensagemEnvelope { }
public class MensagemEnvelopeRemove : MensagemEnvelope { }

public class MensagemEnvelopeUpdate : MensagemEnvelope { }