pipeline {
    agent any

    stages {

        stage('Build') {
            steps {
                echo "Iniciando build"
                bat "echo Build executado com sucesso!"
            }
        }

        stage('Deploy') {
            steps {
                script {
                    def deployPath = "C:\\VinheriaPublicacao"

                    bat """
                    if not exist "${deployPath}\\" (
                        mkdir "${deployPath}"
                    )
                    """

                    bat """
                    xcopy "%WORKSPACE%\\*" "${deployPath}\\" /E /I /Y
                    """
                }
            }
        }
    }

    post {
        success {
            echo "Pipeline finalizado."
        }
        failure {
            echo "Erro no deploy."
        }
    }
}
