# Função para encerrar processos que estão usando uma porta específica
function Free-Port {
    param (
        [int]$port
    )
    Write-Host "Verificando se a porta $port está ocupada..."
    
    # Encontrar o PID do processo que está usando a porta
    $process = netstat -ano | Select-String ":$port" | ForEach-Object { $_.Line.Split()[-1].Trim() } | Select-Object -First 1
    
    if ($process) {
        Write-Host "Encerrando processo com PID $process na porta $port..."
        taskkill /PID $process /F
    } else {
        Write-Host "A porta $port está livre."
    }
}

# Liberar as portas 5001 (backend) e 5173 (frontend)
Free-Port -port 5001
Free-Port -port 5173

# Iniciar o backend
Write-Host "Iniciando o backend..."
Start-Process "dotnet" -ArgumentList "run" -WorkingDirectory "./qualyteam_api"

# Aguardar alguns segundos para garantir que o backend inicie
Start-Sleep -Seconds 5

# Iniciar o frontend
Write-Host "Iniciando o frontend..."
Start-Process "http://localhost:5173"
Set-Location ./qualyteam_frontend
npm run dev

# .\start-dev.ps1